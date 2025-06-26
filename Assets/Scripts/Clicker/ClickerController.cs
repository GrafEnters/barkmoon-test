using System;
using Zenject;
using UnityEngine;
using System.Collections;

public class ClickerController : IInitializable, ITickable, IDisposable
{
    private readonly ClickerModel _model;
    private readonly ClickerView _view;
    private readonly ClickerConfig _config;
    private float _autoCollectTimer;
    private float _energyTimer;
    private bool _disposed;

    public ClickerController(ClickerModel model, ClickerView view, ClickerConfig config)
    {
        _model = model;
        _view = view;
        _config = config;

        _model.Currency = 0;
        _model.Energy = _config.MaxEnergy;
        _view.UpdateCurrency(_model.Currency);
        _view.UpdateEnergy(_model.Energy);
        _view.OnClick += OnClick;
    }

    public void Initialize() {}

    public void Tick()
    {
        if (_disposed) return;
        // Автосбор валюты
        _autoCollectTimer += Time.deltaTime;
        if (_autoCollectTimer >= _config.AutoCollectSeconds)
        {
            _autoCollectTimer = 0f;
            TryAutoCollect();
        }
        // Восстановление энергии
        _energyTimer += Time.deltaTime;
        if (_energyTimer >= _config.EnergyTickSeconds)
        {
            _energyTimer = 0f;
            if (_model.Energy < _config.MaxEnergy)
            {
                _model.Energy = Mathf.Min(_model.Energy + _config.EnergyPerTick, _config.MaxEnergy);
                _view.UpdateEnergy(_model.Energy);
            }
        }
    }

    private void TryAutoCollect()
    {
        if (_model.Energy < _config.AutoCollectCost)
            return;
        _model.Energy -= _config.AutoCollectCost;
        _model.Currency += _config.CurrencyPerClick;
        _view.UpdateCurrency(_model.Currency);
        _view.UpdateEnergy(_model.Energy);
    }

    private void OnClick()
    {
        if (_model.Energy < _config.ClickCost)
            return;
        _model.Energy -= _config.ClickCost;
        _model.Currency += _config.CurrencyPerClick;
        _view.UpdateCurrency(_model.Currency);
        _view.UpdateEnergy(_model.Energy);
        // VFX
        Vector2 screenPos = Input.mousePosition;
        _view.SpawnVFX(screenPos, "+1");
    }

    public void Dispose()
    {
        _disposed = true;
        _view.OnClick -= OnClick;
    }
} 