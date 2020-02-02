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

            minX = (horizontalExtent - bounds.size.x) / 2f + bounds.xMin;
            maxX = bounds.size.x / 2f - horizontalExtent - bounds.xMin;
            minY = verticalExtent - bounds.size.y / 2f + bounds.yMin;
            maxY = bounds.size.y / 2f - verticalExtent - bounds.yMin;

            Debug.Log($"Bounds: {bounds}, verticalExtent: {verticalExtent}, horizontalExtent: {horizontalExtent}, minX: {minX}, maxX: {maxX}, minY: {minY}, maxY: {maxY}");
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