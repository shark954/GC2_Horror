using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gameManager;
    public float fadeDuration = 2.0f; // フェードアウトの持続時間（秒）
    private Material objectMaterial;
    private Color originalColor;
    private float fadeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameManager.goalKey && collision.gameObject.CompareTag("Player"))
        {

            // マテリアルの取得
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                objectMaterial = renderer.material;
                originalColor = objectMaterial.color;
            }
            else
            {
                Debug.LogError("Rendererが見つかりませんでした！");
            }

            if (objectMaterial != null)
            {
                // フェードアウト処理
                fadeTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(originalColor.a, 0, fadeTimer / fadeDuration);

                // マテリアルの色を更新
                objectMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                // アルファ値が0になったらオブジェクトを削除
                if (alpha <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
    

 