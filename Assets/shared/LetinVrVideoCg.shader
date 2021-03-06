﻿Shader "Letin/LetinVrVideoCg"
{
    Properties
    {
        _MainTex("Texture", 2D) = "clear" {}
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Lighting Off
        Cull Off

        Pass
        {
CGPROGRAM
#pragma vertex Vert
#pragma fragment Frag

#include "UnityCG.cginc"

struct VertInput
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct VertToFrag
{
    float4 vertex : SV_POSITION;
    float2 uv : TEXCOORD0;
};

sampler2D _MainTex;

VertToFrag Vert(VertInput i)
{
    VertToFrag o;

    float4 tempVertex = UnityObjectToClipPos(i.vertex);
    o.vertex = float4(i.vertex.xy * tempVertex.w * 2.0, tempVertex.zw);

    float2 tempUv = i.uv;
    o.uv = float2((float(unity_StereoEyeIndex) + tempUv.x) * 0.5, 1.0 - tempUv.y);

    return o;
}

fixed4 Frag(VertToFrag i) : SV_Target
{
    fixed4 col = tex2D(_MainTex, i.uv);
    return col;
}

ENDCG
        }
    }

    Fallback "Unlit/Texture"
}
