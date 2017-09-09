// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32749,y:33257,varname:node_2865,prsc:2|emission-3875-OUT;n:type:ShaderForge.SFN_TexCoord,id:4219,x:31838,y:33202,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2dAsset,id:4430,x:31880,y:33451,ptovrint:False,ptlb:MainTex,ptin:_MainTex,cmnt:MainTex contains the color of the scene,varname:node_9933,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7542,x:32111,y:33310,varname:node_1672,prsc:2,ntxv:0,isnm:False|UVIN-4219-UVOUT,TEX-4430-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:4324,x:31880,y:33661,ptovrint:False,ptlb:ProcessedGlow,ptin:_ProcessedGlow,varname:node_4324,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Subtract,id:1594,x:32359,y:33310,varname:node_1594,prsc:2|A-7148-RGB,B-7542-RGB;n:type:ShaderForge.SFN_Tex2d,id:7148,x:32159,y:33530,varname:node_7148,prsc:2,ntxv:0,isnm:False|UVIN-4219-UVOUT,TEX-4324-TEX;n:type:ShaderForge.SFN_Max,id:3875,x:32549,y:33310,varname:node_3875,prsc:2|A-1594-OUT,B-4389-OUT;n:type:ShaderForge.SFN_Vector1,id:4389,x:32346,y:33470,varname:node_4389,prsc:2,v1:0;proporder:4430-4324;pass:END;sub:END;*/

Shader "Shader Forge/GlowCompositeShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _ProcessedGlow ("ProcessedGlow", 2D) = "bump" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _ProcessedGlow; uniform float4 _ProcessedGlow_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_7148 = tex2D(_ProcessedGlow,TRANSFORM_TEX(i.uv0, _ProcessedGlow));
                float4 node_1672 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = max((node_7148.rgb-node_1672.rgb),0.0);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
