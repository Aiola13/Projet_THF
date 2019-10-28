 Shader "Custom/TestShader03" {
     Properties{
         _MainTex("Base (RGB)", 2D) = "white" {}
         _MainTex2("Text 2 (RGB)", 2D) = "white" {}
         _MainTex3("Text 3 (RGB)", 2D) = "white" {}
         _MainTex4("Text 4 (RGB)", 2D) = "white" {}
 
         _MainTex1_YScrollSpeed("Y Scroll Speed", Float) = 1
         _MainTex2_YScrollSpeed("Y Scroll Speed", Float) = 1
         _MainTex3_YScrollSpeed("Y Scroll Speed", Float) = 1
         _MainTex4_YScrollSpeed("Y Scroll Speed", Float) = 1
     }
 
     SubShader{
         AlphaTest GEqual[_Cuttoff]
 
         Tags{ "RenderType" = "Opaque" }
         LOD 200
  
         CGPROGRAM
         #pragma surface surf Lambert
  
         sampler2D _MainTex;
         sampler2D _MainTex2;
         sampler2D _MainTex3;
         sampler2D _MainTex4;
 
         float _MainTex1_YScrollSpeed;
         float _MainTex2_YScrollSpeed;
         float _MainTex3_YScrollSpeed;
         float _MainTex4_YScrollSpeed;
  
         struct Input {
             float2 uv_MainTex;
             float2 uv_MainTex2;
             float2 uv_MainTex3;
             float2 uv_MainTex4;
         };
  
         void surf(Input IN, inout SurfaceOutput o) {
             
             fixed2 scrollUV_1 = IN.uv_MainTex;
             fixed yScrollValue_1 = -_MainTex1_YScrollSpeed * _Time.x;
             scrollUV_1 += fixed2(0, yScrollValue_1);
 
             fixed2 scrollUV_2 = IN.uv_MainTex2;
             fixed yScrollValue_2 = -_MainTex2_YScrollSpeed * _Time.x;
             scrollUV_2 += fixed2(0, yScrollValue_2);
 
             fixed2 scrollUV_3 = IN.uv_MainTex3;
             fixed yScrollValue_3 = -_MainTex3_YScrollSpeed * _Time.x;
             scrollUV_3 += fixed2(0, yScrollValue_3);
 
             fixed2 scrollUV_4 = IN.uv_MainTex4;
             fixed yScrollValue_4 = -_MainTex4_YScrollSpeed * _Time.x;
             scrollUV_4 += fixed2(0, yScrollValue_4);
 
             half4 tex = tex2D(_MainTex, scrollUV_1);
             half4 tex2 = tex2D(_MainTex2, scrollUV_2);
             half4 tex3 = tex2D(_MainTex3, scrollUV_3);
             half4 tex4 = tex2D(_MainTex4, scrollUV_4);
 
             half3 temp1 = lerp (tex.rgb, tex2.rgb, tex2.a*1);
             half3 temp2 = lerp (temp1, tex3.rgb, tex3.a*1);
             half3 c = lerp (temp2, tex4.rgb, tex4.a*1);
 
             o.Albedo = c.rgb;
 
         }
         ENDCG
     }
     FallBack "Diffuse" 
 }