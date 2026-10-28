using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AddUnitBtn : MonoBehaviour
{
    private int _maxUnitCount = 14;
    private int _unitCount = 1;
    private int _wCount;
    [SerializeField] private int _price;
    [SerializeField] private float _unitCooldown;
    [SerializeField] private Image _icon;
    [SerializeField] private Wheat _wh;
    [SerializeField] private Transform _place;
    [SerializeField] private GameObject _prefab;
    private bool _isCooldown = false;
    private bool _isIcon = false;

    void Update()
    {
        _wCount = _wh.wheatCount;
        if (_isIcon)
        {
            _icon.fillAmount -= 1.0f / _unitCooldown * Time.deltaTime;
        }  
        if (_isCooldown)
        {
            _wCount -= _price;
            _wh.wheatCount -= _price;
            _wh.wheatText.text = $"Пщеница: {_wCount}";
            GameObject newUnit = Instantiate(_prefab, _place);
            _unitCount++;
            _isCooldown = false;
            _isIcon = false;
            StopAllCoroutines();
        }
    }

    public void AddUnit()
    {
        if ((_unitCount != _maxUnitCount || _unitCount! > _maxUnitCount) && (_price <= _wCount))
        {
            StartCoroutine(UnitInit(_unitCooldown));
        }
    }

    IEnumerator UnitInit(float _unitCooldown)
    {
        _isIcon = true;
        yield return new WaitForSeconds(_unitCooldown);
        _isCooldown = true;
        _icon.fillAmount = 1.0f;
    }
}
