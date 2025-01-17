using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gameManager;
    public float fadeDuration = 2.0f; // �t�F�[�h�A�E�g�̎������ԁi�b�j
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

            // �}�e���A���̎擾
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                objectMaterial = renderer.material;
                originalColor = objectMaterial.color;
            }
            else
            {
                Debug.LogError("Renderer��������܂���ł����I");
            }

            if (objectMaterial != null)
            {
                // �t�F�[�h�A�E�g����
                fadeTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(originalColor.a, 0, fadeTimer / fadeDuration);

                // �}�e���A���̐F���X�V
                objectMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                // �A���t�@�l��0�ɂȂ�����I�u�W�F�N�g���폜
                if (alpha <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
    

 