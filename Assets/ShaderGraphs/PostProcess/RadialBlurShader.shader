
Shader "Custom/RadialBlurShader" { 
    Properties { 
        _MainTex ("Base (RGB)", 2D) = "white" {} 
        fSampleDist("fSampleDist", float) = 1
        fSampleStrength("fSampleStrength", float) = 1
    }
    
    SubShader 
    { 
        Pass 
        { 
            ZTest Always Cull Off ZWrite Off 
            Fog { Mode off }
    
    CGPROGRAM 
    // Upgrade NOTE: excluded shader from Xbox360 and OpenGL ES 2.0 because it uses unsized arrays 
    #pragma exclude_renderers xbox360 gles 
    #pragma exclude_renderers xbox360 
    #pragma target 3.0 
    #pragma vertex vert 
    #pragma fragment frag 
    #pragma fragmentoption ARB_precision_hint_fastest
    
    #include "UnityCG.cginc"
    
    uniform sampler2D _MainTex; 
    uniform float fSampleDist; 
    uniform float fSampleStrength; 
    uniform float4 _MainTex_ST; 
    uniform float4 _MainTex_TexelSize;
    //static const float sam[11] = { -0.08,-0.05,-0.03,-0.02,-0.01,0,0.01,0.02,0.03,0.05,0.08 };
    
    struct v2f { 
        float4 pos : POSITION; 
        float2 uv : TEXCOORD0; 
    };
    
    v2f vert (appdata_img v) 
    { 
        v2f o; 
        o.pos = UnityObjectToClipPos(v.vertex); 
        o.uv = v.texcoord.xy; 
        return o; 
    }
    
    float4 frag (v2f i) : COLOR 
    { 
        float2 texCo = i.uv;
        float2 dir = 0.5 - texCo; 
        float dist = length(dir);
      
        dir /= dist; 
        float4 color = tex2D(_MainTex, texCo); 
        float4 sum = color;
    
        float samples[11] = { -0.08,-0.05,-0.03,-0.02,-0.01,0,0.01,0.02,0.03,0.05,0.08 };
    
    
    //    float samples[10] = float[](-0.08,-0.05,-0.03,-0.02,-0.01,0.01,0.02,0.03,0.05,0.08); 
    
        for (int i = 0; i < 10; ++i)  
        {  
            sum += tex2D(_MainTex, texCo + dir*samples[i]*fSampleDist);
    //         sum += tex2D(_MainTex, texCo + dir*samples[i]*0.5);  
        }
    
        sum /= 11.0; 
        float t = saturate(dist * fSampleStrength); 
    //     float t = saturate(dist * 1);
        return lerp(color, sum,t); 
    //return float4 (1,0,0,1);
    } 
    ENDCG
    
        } 
    }
    
    Fallback off
    
    }