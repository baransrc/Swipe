using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        public SpriteRenderer navbarBottomSprite;
        public TextMeshProUGUI scoreText;
        public GameController gameController;

        public void Initialize()
        {
            navbarBottomSprite.color = gameController.colorPalette.colorless;
            scoreText.color = gameController.colorPalette.colorless;
            SetScoreText("");
        }

        public void SetScoreText(string text)
        {
            scoreText.SetText(text);
        }
    }
}