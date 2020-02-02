using System.Data;
using Global;
using UnityEngine;

namespace Player {
    public class FollowPlayer : MonoBehaviour {
        private GameObject player;

        private float minX;
        private float maxX;
        private float minY;
        private float maxY;

        // Start is called before the first frame update
        void Start() {
            this.player = GameObject.FindWithTag("Player");
            if (player == null) throw new EvaluateException("No player found in the Scene.");

            var bounds = GridSystem.Instance.GroundBounds();
            var verticalExtent = Camera.main.orthographicSize;
            var horizontalExtent = verticalExtent * Screen.width / Screen.height;

            minX = bounds.xMin + horizontalExtent - 0.5f;
            maxX = bounds.xMax - horizontalExtent - 0.5f;
            minY = bounds.yMin + verticalExtent - 0.5f;
            maxY = bounds.yMax - verticalExtent - 0.5f;

            Debug.Log($"Bounds: {bounds}, verticalExtent: {verticalExtent}, horizontalExtent: {horizontalExtent}, minX: {bounds.xMin}, maxX: {bounds.xMax}, minY: {bounds.yMin}, maxY: {bounds.yMax}");
        }

        // Update is called once per frame
        void LateUpdate() {
            var position = player.transform.position;
            position.z = -10;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            transform.position = position;
        }
    }
}