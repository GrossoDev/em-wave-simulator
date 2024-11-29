Shader "FieldVisualizer"
{
    Properties
    {
        _Brightness("Brightness", Float) = 1.0
        _TimeScale("Time Scale", Float) = 0.01
        _C("Speed of Light", Float) = 1.0
        _Scale("Scale", Float) = 1.0
        _IntensityDecayPower("Intensity decay power (Inverse Square Law)", Float) = 2.0
        [MaterialToggle] _IsGrayscale("Display in Grayscale", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            // Constants
            #define PI 3.1415296535
            #define MAX_SOURCES 50

            // Properties
            float _Brightness;
            float _TimeScale;
            float _C;
            float _Scale;
            float _IsGrayscale;
            float _IntensityDecayPower;

            // Source Parameters
            float2 _SourcePositions[MAX_SOURCES];
            float _SourceAmplitudes[MAX_SOURCES];
            float _SourceFrequencies[MAX_SOURCES];
            float _SourcePhases[MAX_SOURCES];
            int _SourceCount; // Actual number of sources

            // Vertex structure
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 fragPos = i.worldPos;
                float t = _Time.y * _TimeScale;

                float value = 0.0;
                for (int j = 0; j < _SourceCount; j++)
                {
                    float2 position = _SourcePositions[j];
                    float amplitude = _SourceAmplitudes[j];
                    float frequency = _SourceFrequencies[j];
                    float phase = _SourcePhases[j];

                    float wave_length = _C / _SourceFrequencies[j];
                    float dist = length(position - fragPos) * _Scale;
                    float contribution = amplitude * sin(2.0 * PI * (dist / wave_length - frequency * t) + phase) / pow(1.0 + dist, _IntensityDecayPower);

                    value += contribution;
                }

                value *= _Brightness;

                if (_IsGrayscale)
                    return abs(float4(value, value, value, 1.0));
                else
                    return float4(value, 0.0, -value, 1.0);
            }
            ENDCG
        }
    }
}
