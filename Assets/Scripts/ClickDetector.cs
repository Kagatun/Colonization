using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayoutBase _layoutBase;
    [SerializeField] private LayerMask _layerMaskMars;
    [SerializeField] private LayerMask _layerMaskIgnoreRaycast;

    private GameObject _previousHitObject;
    private Beacon _selectedBeacon;
    private Base _selectedBase;
    private bool _isEffectActive = false;

    private void Update()
    {
        if (_isEffectActive)
            FollowCursor();

        if (_selectedBeacon != null && _selectedBeacon.CanBeMoved == false)
        {
            TurnOffCursorTrackingEffect();
        }
    }

    private void OnEnable()
    {
        _inputDetector.LeftClicked += OnHandleLeftClick;
        _inputDetector.RightClicked += OnHandleRightClick;
    }

    private void OnDisable()
    {
        _inputDetector.LeftClicked -= OnHandleLeftClick;
        _inputDetector.RightClicked -= OnHandleRightClick;
    }

    private void OnHandleLeftClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~_layerMaskIgnoreRaycast))
        {
            if (hit.transform.TryGetComponent(out Base @base))
            {
                if (_isEffectActive && _selectedBase == @base)
                {
                    TurnOffCursorTrackingEffect();

                    return;
                }

                ResetSelect();

                _selectedBase = @base;
                _selectedBeacon = @base.Beacon;

                if (_selectedBeacon != null && _selectedBeacon.CanBeMoved)
                    _isEffectActive = true;

                SwapCursor(hit);
            }

            if (_isEffectActive && ((1 << hit.transform.gameObject.layer) & _layerMaskMars) != 0 && _layoutBase.CanInstall && _selectedBeacon != null && _selectedBeacon.CanBeMoved)
                ToPutBeacon(hit);
        }
    }

    private void OnHandleRightClick()
    {
        if (_isEffectActive && _selectedBeacon != null && _selectedBeacon.CanBeMoved)
            TurnOffBeacon();
    }

    private void FollowCursor()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = Mathf.Clamp(hitPoint.y, 0f, 0.1f);
            _layoutBase.transform.position = hitPoint;

            if (hit.transform.gameObject != _previousHitObject)
                _previousHitObject = hit.transform.gameObject;
        }
        else
        {
            _previousHitObject = null;
        }
    }

    private void SwapCursor(RaycastHit hit)
    {
        if (_isEffectActive)
        {
            TurnOnCursorTrackingEffect(hit);
        }
        else if (_selectedBeacon != null && !_selectedBeacon.IsActivated)
        {
            TurnOffCursorTrackingEffect();
        }
    }

    private void ResetSelect()
    {
        _selectedBeacon = null;
        _selectedBase = null;
    }

    private void ToPutBeacon(RaycastHit hit)
    {
        TurnOffCursorTrackingEffect();
        _selectedBeacon.gameObject.SetActive(true);
        _selectedBeacon.transform.position = hit.point;
        _selectedBeacon.AllowInstallation();
    }

    private void TurnOffBeacon()
    {
        TurnOffCursorTrackingEffect();
        _selectedBeacon.gameObject.SetActive(false);
        _selectedBeacon.ProhibitInstallation();
    }

    private void TurnOnCursorTrackingEffect(RaycastHit hit)
    {
        _layoutBase.gameObject.SetActive(true);
        _layoutBase.transform.position = hit.transform.position;
        _isEffectActive = true;
    }

    private void TurnOffCursorTrackingEffect()
    {
        _layoutBase.gameObject.SetActive(false);
        _isEffectActive = false;
    }
}
