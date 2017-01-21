using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class DrawCircle : MonoBehaviour
{
    public enum Axis { X, Y, Z };

    [SerializeField]
    [Tooltip("The number of lines that will be used to draw the circle. The more lines, the more the circle will be \"flexible\".")]
    [Range(0, 1000)]
    private int _segments = 60;

    [SerializeField]
    [Tooltip("The radius of the horizontal axis.")]
    public float _horizRadius = 10;

    [SerializeField]
    [Tooltip("The radius of the vertical axis.")]
    public float _vertRadius = 10;

    [SerializeField]
    [Tooltip("The offset will be applied in the direction of the axis.")]
    private float _offset = 0;

    [SerializeField]
    [Tooltip("The axis about which the circle is drawn.")]
    private Axis _axis = Axis.Z;

    [SerializeField]
    [Tooltip("If checked, the circle will be rendered again each time one of the parameters change.")]
    private bool _checkValuesChanged = true;

    private int _previousSegmentsValue;
    private float _previousHorizRadiusValue;
    private float _previousVertRadiusValue;
    private float _previousOffsetValue;
    private Axis _previousAxisValue;

    private LineRenderer _line;

    public float random;

    void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();

        _line.numPositions = _segments + 1;
        _line.useWorldSpace = false;

        UpdateValuesChanged();

        CreatePoints();
    }

    void Update()
    {
        if (_checkValuesChanged)
        {
            if (_previousSegmentsValue != _segments ||
                _previousHorizRadiusValue != _horizRadius ||
                _previousVertRadiusValue != _vertRadius ||
                _previousOffsetValue != _offset ||
                _previousAxisValue != _axis)
            {
                CreatePoints();
            }

            UpdateValuesChanged();
        }
    }

    void UpdateValuesChanged()
    {
        _previousSegmentsValue = _segments;
        _previousHorizRadiusValue = _horizRadius;
        _previousVertRadiusValue = _vertRadius;
        _previousOffsetValue = _offset;
        _previousAxisValue = _axis;
    }
    [SerializeField]float scale;
    [SerializeField]float frequency;
    [SerializeField]float amplitude;

    void CreatePoints()
    {
        

        if (_previousSegmentsValue != _segments)
        {
            _line.numPositions = _segments + 1;
        }

        float x;
        float y;
        float z = _offset;

        float angle = 0f;

        for (int i = 0; i < (_segments + 1); i++)
        {
            float perlin = Mathf.PerlinNoise(i / scale * frequency + random * 10, i / scale * frequency + random);
            float height = perlin * amplitude / 100;
            //float xPerl = Mathf.Sin(i / sinWaveAmp) + sinWaveFrequency;
            //float yPerl = Mathf.Sin(i / sinWaveAmp) + sinWaveFrequency;

            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _horizRadius * Mathf.PerlinNoise(height, height);/*((Mathf.Cos(i /sinWaveAmp) + sinWaveAmp2)*/
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * _vertRadius * Mathf.PerlinNoise(height, height);

            switch (_axis)
            {
                case Axis.X:
                    _line.SetPosition(i, new Vector3(z, y, x));
                    break;
                case Axis.Y:
                    _line.SetPosition(i, new Vector3(y, z, x));
                    break;
                case Axis.Z:
                    _line.SetPosition(i, new Vector3(x, y, z));
                    break;
                default:
                    break;
            }

            angle += (360f / _segments);
        }

    }
}