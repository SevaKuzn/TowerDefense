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
    private bool _isCooldown = false;
    private bool _isIcon = false;

    void Update()
    {
        _wCount = _wh.wheatCount;
        StartCoroutine(Timer(_unitCooldown));
        if (_isIcon)
        {
            _icon.fillAmount -= 1.0f / _unitCooldown * Time.deltaTime;
            _isIcon = false;
        }

    }

    public void AddUnit(GameObject prefab)
    {
        if ((_unitCount != _maxUnitCount || _unitCount! > _maxUnitCount) && (_price <= _wCount))
        {
            if (_isCooldown)
            {
                _wCount -= _price;
                _wh.wheatCount -= _price;
                _wh.wheatText.text = $"Пщеница: {_wCount}";
                GameObject newUnit = Instantiate(prefab, _place);
                _unitCount++;
                _isCooldown = false;
                StopAllCoroutines();
            }
        }
    }

    IEnumerator Timer(float _unitCooldown)
    {
        _isIcon = true;
        yield return new WaitForSeconds(_unitCooldown);
        _isCooldown = true;
        _icon.fillAmount = 1.0f;
    }
}
