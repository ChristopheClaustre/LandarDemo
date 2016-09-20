using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightStateGenerator : MonoBehaviour {

    // Prend en entrer une liste de lumiere et retourne la quantité de lumiere emise par le GameObject porteur du script
    public float calculLight (List<Light>  lampes) {

        float lumiereRetoure = 0.0f;
        float lumiereAmbient = 0.0f;
        RaycastHit hit = new RaycastHit();

        Vector3 lumiereVersObjet;
        Vector3 axeLampe;
        float angleLampe;

        for (int i = 0; i < lampes.Count; i++)
        {
            lumiereAmbient += lampes[i].intensity;
            lumiereVersObjet = this.transform.position - lampes[i].transform.position;
            if (lampes[i].type == LightType.Spot)
            {
                axeLampe = lampes[i].transform.eulerAngles;
                angleLampe = lampes[i].spotAngle;
                //Si on est dans le cone d'emission du spot
                if (angleLampe > Vector3.Angle(axeLampe, lumiereVersObjet))
                {
                    //Attention toute modification doit etre reporté sur les autres cas !!
                    if (Physics.Raycast(lampes[i].transform.position, lumiereVersObjet, out hit, lumiereVersObjet.magnitude))
                    {
                        //Si le rayon touche l'objet il existe un eclairage direct
                        if (hit.transform.gameObject == this)
                        {
                            lumiereRetoure += lampes[i].intensity;
                        }
                    }
                }
            }
            else if (lampes[i].type == LightType.Point)
            {
                if (Physics.Raycast(lampes[i].transform.position, lumiereVersObjet, out hit, lumiereVersObjet.magnitude))
                {
                    //Si le rayon touche l'objet il existe un eclairage direct
                    if (hit.transform.gameObject == this)
                    {
                        lumiereRetoure += lampes[i].intensity;
                    }
                }
            }
        }
        lumiereRetoure = lumiereRetoure + lumiereAmbient * 0.1f;
        return lumiereRetoure;
    }
}
