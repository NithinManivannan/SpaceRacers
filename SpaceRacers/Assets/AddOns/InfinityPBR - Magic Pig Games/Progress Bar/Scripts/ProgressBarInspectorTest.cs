using System;
using UnityEngine;

namespace MagicPigGames
{
    public class ProgressBarInspectorTest : MonoBehaviour
    {
        [Header("Test Zone")]
        [Tooltip("Toggle on to test the progress bar in the editor, during play mode.")]
        public bool enableTesting = true;
        [Range(0f, 1f)]
        [Tooltip("Note, if testing in the editor and invertProgress is true, the progress value will be inverted.")]
        public float progress = 1f; // This is the Inspector test value for progress!
      
        private float _lastProgress = 1f;
        private VerticalProgressBar _progressBar;

        public virtual void Update()
        {
            

            if (!enableTesting) return;
            if (Math.Abs(_lastProgress - progress) < 0.001) return;

            _lastProgress = progress;
            _progressBar.SetProgress(progress);
        }

        private void OnValidate()
        {
            if (_progressBar == null)
                _progressBar = GetComponent<VerticalProgressBar>();
        }
        public void SetProgress(float value)
        {
            if (_progressBar != null)
            {
                _progressBar.SetProgress(value);
            }
        }
    }
}