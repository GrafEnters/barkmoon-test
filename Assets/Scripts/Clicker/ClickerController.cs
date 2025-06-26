using Zenject;

public class ClickerController
{
    private readonly ClickerModel _model;
    private readonly ClickerView _view;
    private readonly ClickerConfig _config;

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

    private void OnClick()
    {
        if (_model.Energy < _config.ClickCost)
            return;
        _model.Energy -= _config.ClickCost;
        _model.Currency += _config.CurrencyPerClick;
        _view.UpdateCurrency(_model.Currency);
        _view.UpdateEnergy(_model.Energy);
    }
} 