// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// FogOfWarVertex shader
// Copyright (C) 2013 Sergey Taraban <http://staraban.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

Shader "Custom/FogOfWarGeom" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Color (RGB) Alpha (A)", 2D) = "white"
        _FogRadius ("FogRadius", Float) = 1.0
        _FogMaxRadius ("FogMaxRadius", Float) = 0.5
        _Player1_Pos ("UnitPos1", Vector) = (0,0,0,1)
        _Player2_Pos ("UnitPos2", Vector) = (0,0,0,1)
        _Player3_Pos ("UnitPos3", Vector) = (0,0,0,1)
    }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite On
        Cull Off 

        CGPROGRAM
        #pragma target 3.0
        #pragma surface surf Lambert vertex:vert alpha:blend

        sampler2D _MainTex;
        fixed4 _Color;
        float4 _Player1_Pos;
        float4 _Player2_Pos;
        float4 _Player3_Pos;
        float _FogRadius;
        float _FogMaxRadius;

        struct Input {
            float4 pos;
            float alpha;
            float2 uv_MainTex;
            float2 location;
        };
        

        float powerForPos(float4 pos, float2 nearVertex);

        void vert (inout appdata_full vertexData, out Input outData)
        {
        	UNITY_INITIALIZE_OUTPUT(Input,outData);
            //float2 posVertex = mul (_Object2World, vertexData.vertex);
            float4 posWorld = mul(unity_ObjectToWorld, vertexData.vertex);
            outData.uv_MainTex = vertexData.texcoord;
            outData.location = posWorld.xz;
        }

        void surf (Input IN, inout SurfaceOutput o) {
        	fixed4 baseColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;
        	float alpha = (1.0 - (baseColor.a + powerForPos(_Player1_Pos, IN.location) + powerForPos(_Player2_Pos, IN.location) + powerForPos(_Player3_Pos, IN.location)));
            o.Albedo = baseColor.rgb;
            o.Alpha = alpha;
        }

        float powerForPos(float4 pos, float2 nearVertex) {
             float attenumat = clamp(_FogRadius - abs(length(pos.xz - nearVertex.xy)), 0.0, _FogRadius);
             if(attenumat > _FogRadius*_FogMaxRadius) {
                 attenumat = _FogRadius*_FogMaxRadius;  
             }
            return (1.0/_FogMaxRadius)*(attenumat/_FogRadius);
        }
        
        ENDCG
    }

Fallback "Transparent/VertexLit"
}