Shader "Custom/CuttingShaderTextured"
{
    Properties
    {
        _OutColor ("Outside Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_OutTexture ( "Outside Color Map", 2D ) = "white" {}
		[Normal] OutBumpMap ("Outside Normal Map", 2D) = "bump" {}

		[Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
		_OutMetalMap ( "Outside Metal Map", 2D ) = "white" {}

		_OutRoughMap ( "Outside Rough Map", 2D ) = "white" {}
		_OutAOMap ( "Outside Ambient Map", 2D ) = "white" {}

		_SecColor ( "Section color", Color) = (1.0, 1.0, 1.0, 1.0)

		_EdgeWidth("Edge width", Range(1, 0)) = 0.9
		_Val("World Height value", Float ) = 1
    }

    SubShader
    {
		Tags { "Queue" = "Geometry" }
		Tags { "RenderType" = "Opaque" }
		LOD 200

		//  PASS 1
		CGPROGRAM

		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0


		sampler2D _OutTexture;
		sampler2D OutBumpMap;
		sampler2D _OutMetalMap;
		sampler2D _OutRoughMap;
		sampler2D _OutAOMap;

		struct Input {
			float2 uv_OutTexture;
			float2 uvOutBumpMap;
			float3 worldPos;
		};

		fixed4 _OutColor;
		float _Val;
		float _Metallic;

		void surf ( Input IN, inout SurfaceOutputStandard o )
		{
			if (IN.worldPos.y > _Val)
				discard;

			fixed4 occ = tex2D ( _OutAOMap, IN.uv_OutTexture );
			fixed4 c = tex2D ( _OutTexture, IN.uv_OutTexture ) * _OutColor * occ;
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal (tex2D (OutBumpMap, IN.uvOutBumpMap));

			fixed4 rough = tex2D ( _OutRoughMap, IN.uv_OutTexture );
			fixed4 metal = tex2D ( _OutMetalMap, IN.uv_OutTexture );
			o.Metallic = rough.r * metal.r * _Metallic;
		}

		ENDCG


		//  PASS 2
		Pass{
			
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float4 worldPos : TEXCOORD0;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			
			fixed4 _SecColor ; 
			float _Val;

			fixed4 frag(v2f i) : SV_Target
			{
				if (i.worldPos.y > _Val)
					discard;

				return _SecColor ; 
			}


			ENDCG
        }

		//  PASS 3
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float4 worldPos : TEXCOORD0;
			};

			float _EdgeWidth;

			v2f vert(appdata_base v)
			{
				v2f o;
				v.vertex.xyz *= _EdgeWidth;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			fixed4 _SecColor ; 
			float _Val;

			fixed4 frag(v2f i) : SV_Target {
				if (i.worldPos.y > _Val)
					discard;

				return  _SecColor;
			}

			ENDCG
		}

		//  PASS 4
        Cull Front
 
        CGPROGRAM
        #pragma surface surf Standard vertex:vert

		sampler2D _OutTexture;
		sampler2D _OutAOMap;
		sampler2D OutBumpMap;

        struct Input {
			float2 uv_OutTexture;
			float2 uvOutBumpMap;
            float3 worldPos;
        };
 
        float _EdgeWidth;
 
        void vert(inout appdata_base v)
        {
            v.vertex.xyz *= _EdgeWidth;
        }
 
        fixed4 _OutColor;
        float _Val;
 
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            if ( IN.worldPos.y > _Val )
                discard;

			
            fixed4 occ = tex2D ( _OutAOMap, IN.uv_OutTexture );
			fixed4 c = tex2D(_OutTexture, IN.uv_OutTexture) * _OutColor * occ;
			o.Albedo = c.rgb;

			//o.Normal = UnpackNormal (tex2D (OutBumpMap, IN.uvOutBumpMap));
        }
 
        ENDCG
    }
}