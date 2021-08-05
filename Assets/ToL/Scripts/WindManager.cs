using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindManager : MonoBehaviour
{
    [Header("Assets")]
    public ParticleSystem windEffect;
    //public Terrain mainTerrain;
    public WindZone windZone;
    public DynamicBone[] flowerbones;

    [Header("WindZoneValues_Tree")]
    public float windZoneStrength = 3.5f;
    public float windZoneTurbulence = 1f;
    public float treeTransitionSpeed = 0.1f;

    [Header("WindZoneValues_Grass")]
    public float grassWaveStrength = 0.6f;
    public float grassWaveSpeed = 0.6f;
    public float grassWaveBendForce = 0.4f;
    public float grassTransitionSpeed = 0.02f;

    public float timeToRain = 5f;
    private float currentTimeToRain;
    public UnityEvent OnRain;

    [SerializeField]
    private bool canWind;
    private void OnEnable()
    {
        //mainTerrain.terrainData.wavingGrassStrength = 0.15f;
        //mainTerrain.terrainData.wavingGrassAmount = 0.25f;
        windEffect.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (canWind) {
            windEffect.transform.parent.gameObject.SetActive(true);
            for (int i = 0; i < flowerbones.Length; i++)
            {
                flowerbones[i].enabled = true;
            }

            if (windZone.windMain < windZoneStrength)
            {
                windZone.windMain += Time.deltaTime * treeTransitionSpeed;
            }
            if (windZone.windTurbulence < windZoneTurbulence)
            {
                windZone.windTurbulence += Time.deltaTime * treeTransitionSpeed;
            }
            //if (mainTerrain.terrainData.wavingGrassStrength < grassWaveStrength)
            //{
            //    mainTerrain.terrainData.wavingGrassStrength += Time.deltaTime * grassTransitionSpeed;
            //}
            //if (mainTerrain.terrainData.wavingGrassAmount < grassWaveBendForce)
            //{
            //    mainTerrain.terrainData.wavingGrassAmount += Time.deltaTime * grassTransitionSpeed;
            //}

            if (currentTimeToRain < timeToRain)
            {
                currentTimeToRain += Time.unscaledDeltaTime;
            }
            else
            {
                OnRain?.Invoke();
                this.enabled = false;
            }
        }
    }

    public void WindJoin() {
        canWind = true;
    }
}
