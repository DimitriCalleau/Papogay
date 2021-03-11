using UnityEngine;

public class V_S_FX_Library : MonoBehaviour
{
    #region Singleton
    private static V_S_FX_Library fX_Library = null;

    // Game Instance Singleton
    public static V_S_FX_Library FX_Library
    {
        get
        {
            return fX_Library;
        }
    }

    private void Awake()
    {
        if (fX_Library != null && fX_Library != this)
        {
            Destroy(this.gameObject);
        }
        fX_Library = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    [Header("Sound FX")]
    public AudioSource S_enmSpawn;
    public AudioSource S_endWave, S_startWave, S_cantSummonTrap, S_shop, S_enmAttack, S_dash, S_trapActivated, S_trapDestroyed, S_trapPlaced, S_playerDeath, S_conversion, S_newZoneOnMap;
    [Header("Visual FX")]
    public ParticleSystem V_corpoSpawn;
    public ParticleSystem V_spawnEnm, V_placeTrap, V_convertToAlly, V_convertToEnm, V_convertToNeutral;
    public GameObject V_shopBeam, V_corpoBeam, V_playerDamaged, V_entityDamaged, V_DamagedCorpo;

    #region SFX

    public void SpawnENM()
    {
        //V_enmSpawn.Play();
        S_enmSpawn.Play();
    }

    public void EndWave()
    {
        //V_endWave.Play();
        S_endWave.Play();
    }

    public void StartWave()
    {
        //V_startWave.Play();
        S_startWave.Play();
    }

    public void CantSummonTrap()
    {
        //V_cantSummonTrap.Play();
        S_cantSummonTrap.Play();
    }

    public void Shop()
    {
        //V_shop.Play();
        S_shop.Play();
    }

    public void EnmAttack()
    {
        //V_enmAttack.Play();
        S_enmAttack.Play();
    }

    public void Dash()
    {
        //V_dash.Play();
        S_dash.Play();
    }

    public void TrapOn()
    {
        //V_trapActivated.Play();
        S_trapActivated.Play();
    }

    public void TrapOff()
    {
        //V_trapDestroyed.Play();
        S_trapDestroyed.Play();
    }

    public void PlaceTrap()
    {
        //V_trapPlaced.Play();
        S_trapPlaced.Play();
    }

    public void PlayerDeath()
    {
        //V_playerDeath.Play();
        S_playerDeath.Play();
    }

    public void Conversion()
    {
        //V_conversion.Play();
        S_conversion.Play();
    }

    public void NewZoneAvailable()
    {
        //V_newZoneOnMap.Play();
        S_newZoneOnMap.Play();
    }

    #endregion

    #region VFX
    
    private float timerFloat;

    void FixedUpdate()
    {
        timerFloat -= Time.deltaTime;
        if (timerFloat <= 0)
        {
            timerFloat = 5;
            V_corpoBeam.SetActive(false);
            V_shopBeam.SetActive(false);
            V_playerDamaged.SetActive(false);
            V_entityDamaged.SetActive(false);
        }
    }

    public void SpawnCorpoFX()
    {
        V_corpoSpawn.Play();
    }

    public void ShopBeam()
    {
        V_shopBeam.SetActive(true);
    }

    public void CorpoBeam()
    {
        V_corpoBeam.SetActive(true);
    }

    public void SpawnEnmFX()
    {
        V_spawnEnm.Play();
    }

    public void PlayerDamaged()
    {
        V_playerDamaged.SetActive(true);
    }

    public void EntityDamaged()
    {
        V_entityDamaged.SetActive(true);
    }

    public void PlaceTrapFX()
    {
        V_placeTrap.Play();
    }

    public void ConvertToAlly()
    {
        V_convertToAlly.Play();
    }

    public void ConvertToEnm()
    {
        V_convertToEnm.Play();
    }

    public void ConvertToNeutral()
    {
        V_convertToNeutral.Play();
    }

    public void CorporationDamaged()
    {
        V_DamagedCorpo.GetComponentsInChildren<ParticleSystem>()[0].Play();
        V_DamagedCorpo.GetComponentsInChildren<ParticleSystem>()[1].Play();
    }
    #endregion
}