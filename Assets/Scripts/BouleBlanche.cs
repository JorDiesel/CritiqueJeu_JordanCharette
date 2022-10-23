using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BouleBlanche : MonoBehaviour
{
    [Header("Paramètres de déplacement")]
    public float vitesse;
    private float vitesseRelative;

    private Rigidbody rb;
    private bool peutLancer;
    private float tempsAttente = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        peutLancer = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && peutLancer)
        {
            peutLancer = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.freezeRotation = true;
            //Uniquement multiplier par le négatif de la vitesse
            rb.AddForce(GetDirection() * -vitesseRelative, ForceMode.Acceleration);
            StartCoroutine(AttenteFinTour());

        }
    }

    private Vector3 GetDirection()
    {
        Vector3 positionSouris = Input.mousePosition;
        positionSouris.z = 13.0f;
        
        float normeVecteur = Mathf.Sqrt(Mathf.Pow(positionSouris.x, 2f) + Mathf.Pow(positionSouris.y, 2f) + Mathf.Pow(positionSouris.z, 2f));
        Vector3 vecteurNormal = (positionSouris / normeVecteur);

        vecteurNormal = vecteurNormal * 10;
        vecteurNormal.y = 4.15f;
        //Multiplier le vecteur normal par la valeur du slider
        //Remplacer le positionSouris par le vecteur normal dans le return
        Vector3 direction = rb.transform.position - Camera.main.ScreenToWorldPoint(positionSouris);
        Vector3 testNormalisation = rb.transform.position - Camera.main.ScreenToWorldPoint(vecteurNormal);
        return direction;

    }

    public void SetVitesse(float sliderValue)
    {
        vitesseRelative = vitesse * sliderValue; 
    }

    public IEnumerator AttenteFinTour()
    {
        float tempsEcoule = 0f;
        while (tempsEcoule < tempsAttente)
        {
            tempsEcoule += Time.deltaTime;
            yield return null;
        }

        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;

        while (rb.velocity != Vector3.zero)
        {
            tempsEcoule += Time.deltaTime;
            yield return null;
        }
        
        peutLancer = true;
    }
}
