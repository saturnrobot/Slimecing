using UnityEngine;

namespace Slimecing.Dependency
{
    public class Ellipse
    {
        private readonly float _xAxis;
        private readonly float _yAxis;

        public Ellipse(float xAxis, float yAxis)
        {
            _xAxis = xAxis;
            _yAxis = yAxis;
        }

        public Vector2 EvaluateEllipse(float pos)
        {
            float angle = Mathf.Deg2Rad * 360f * pos;
            float x = Mathf.Sin(angle) * _xAxis;
            float y = Mathf.Cos(angle) * _yAxis;
            
            return new Vector2(x, y);
        }
    }
}
