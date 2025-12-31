using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Data")]
    [SerializeField] private int MaxHP = 100;
    public int CurrentHP { get; private set; }
    public int Coins { get; private set; }
    public bool HasKey { get; private set; }


    [SerializeField] private Animator doorAnimator;


    [Header("Coin Sound Settings")]
    [SerializeField] private AudioSource collectableSound;
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.1f;

    public event Action<int> OnHPChanged;
    public event Action<int> OnCoinsChanged;
    public event Action<bool> OnKeyChanged;

    public bool IsStunned { get; set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetHP(MaxHP);
    }

    public void ResetHP()
    {
       CurrentHP = MaxHP;
    }
    public void TakeDamage(int amount)
    {
        if (IsStunned) return; 
        SetHP(CurrentHP - amount);
    }

    public void SetHP(int value)
    {
        CurrentHP = Mathf.Clamp(value, 0, MaxHP);
        OnHPChanged?.Invoke(CurrentHP);
    }

    public void ResetCoin()
    {
        Coins = 0;
        OnCoinsChanged?.Invoke(Coins);
    }

    public void AddCoin(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
    }

    public void SetKey(bool value)
    {
        HasKey = value;
        OnKeyChanged?.Invoke(HasKey);
    }

    public bool CheckIfHasKey()
    {
        if (!HasKey) return false;

        HasKey = false;
        return true;

    }


    public void PlayCollectableSound()
    {
        if (collectableSound == null) return;

        collectableSound.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
        collectableSound.Play();
    }


}
