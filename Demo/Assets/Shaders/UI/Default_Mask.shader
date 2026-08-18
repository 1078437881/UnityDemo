Shader "Unlit/Default_Mask"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color("Tint",Color) = (0,0,0,0.75)

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID",Float) = 0
        _StencilOP("Stencil Operation",Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask",Float) = 255

        _ColorMask("Color Mask",Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip",Float) = 0

        _Center("Center",Vector) = (0,0,0,0)
        _Slider("_Slider",Range(0,1000)) = 1000
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOP]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusDstAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #pragma multi_compile _ UNITY_UI_ALPHACLIP

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;

            float _Slider;
            float4 _Center;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = IN.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.color = IN.color * _Color;
                OUT.texcoord = IN.texcoord;
                return OUT;
            }

            sampler2D _MainTex;
            
            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex,IN.texcoord)+ _TextureSampleAdd)*IN.color;

                color.a *=UnityGet2DClipping(IN.worldPosition.xy,_ClipRect);

                #ifdef UNITY_UI_ALPHACLIP
                clip(color.a-0.001);
                #endif

                float dist = distance(IN.worldPosition.xy, _Center.xy);
                // 圈内透明挖洞，外面保留蒙版黑色
                if(dist < _Slider)
                {
                    color.a = 0;
                }

                color.rgb *= color.a;
                return color;
            }
            ENDCG
        }
    }
}