using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Description:    Scrolls content (Text, pictures, etc.) verticaly within a viewport/mask.
/// Author:         Marc Fischer
/// </summary>
public class VerticalAutoContentScroller : MonoBehaviour
{
    [SerializeField] private RectTransform _targetRectTransform;
    [SerializeField] private RectTransform _maskRectTransform;
    [SerializeField] private float _delayBeforeScroll;
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private StartDirection _startDirection = StartDirection.Up;
    [SerializeField] private ScrollStyle _scrollStyle = ScrollStyle.FlipFlop;

    private float _direction = -1f; // Should be -1 (Up) or 1 (Down)
    private float _initialYPosition;
    private float _maskBottom;
    private float _maskTop;
    private float _targetBoundDelta;
    private float _currentYPosition;

    private bool _isUsingText;
    private TextMeshProUGUI _textMeshProComponent;
    private enum StartDirection
    {
        Down,
        Up
    }

    private enum ScrollStyle
    {
        FlipFlop,
        Loop
    }

    private void Awake()
    {
        _textMeshProComponent = _targetRectTransform.GetComponent<TextMeshProUGUI>();
        _isUsingText = (_textMeshProComponent != null);
    }

    private void Start()
    {
        _direction = (_startDirection == StartDirection.Down) ? 1f : -1f;
        _initialYPosition = _targetRectTransform.anchoredPosition.y;
        _currentYPosition = 0f;
        _maskBottom = _maskRectTransform.anchoredPosition.y - _maskRectTransform.sizeDelta.y * 0.5f;
        _maskTop = _maskRectTransform.anchoredPosition.y + _maskRectTransform.sizeDelta.y * 0.5f;

        if (_isUsingText)
        {
            _targetBoundDelta = _textMeshProComponent.preferredHeight * 0.5f;
        }
        else
        {
            _targetBoundDelta = _targetRectTransform.sizeDelta.y * 0.5f;
        }
    }

    private void OnEnable()
    {
        switch (_scrollStyle)
        {
            case ScrollStyle.FlipFlop:
                StartCoroutine(ScrollContentFlipFlop(_delayBeforeScroll));
                break;
            case ScrollStyle.Loop:
                StartCoroutine(ScrollContentLoop(_delayBeforeScroll));
                break;
        }
    }

    private void OnDisable()
    {
        ResetValues();
    }


    /// <summary>
    /// Description:    Resets the scrolling values to their initial state.
    /// Author:         Marc Fischer
    /// </summary>
    private void ResetValues()
    {
        _currentYPosition = 0f;
        _direction = (_startDirection == StartDirection.Down) ? 1f : -1f;
        _targetRectTransform.anchoredPosition = new Vector2(_targetRectTransform.anchoredPosition.x, _initialYPosition);
    }

    /// <summary>
    /// Description:    Scrolls the content in a flip-flop style.
    /// Author:         Marc Fischer
    /// </summary>
    private IEnumerator ScrollContentFlipFlop(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            float currentTargetBottom = _currentYPosition - _targetBoundDelta - _initialYPosition * _direction;
            float currentTargetTop = _currentYPosition + _targetBoundDelta + _initialYPosition * _direction;
            if ((currentTargetTop < _maskBottom && _direction < 0) || (currentTargetBottom > _maskTop && _direction > 0))
            {
                yield return new WaitForSeconds(delay);
                _direction *= -1;
            }

            _currentYPosition += _scrollSpeed * Time.deltaTime * _direction;
            _targetRectTransform.anchoredPosition = new Vector2(_targetRectTransform.anchoredPosition.x, _initialYPosition - _currentYPosition);

            yield return null;
        }
    }

    /// <summary>
    /// Description:    Scrolls the content in a loop style.
    /// Author:         Marc Fischer
    /// </summary>
    private IEnumerator ScrollContentLoop(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            float currentTargetTop = _currentYPosition + _targetBoundDelta + _initialYPosition * _direction;
            if (currentTargetTop < _maskBottom && _direction < 0)
            {
                _currentYPosition = _maskRectTransform.sizeDelta.y - _initialYPosition;
                _targetRectTransform.anchoredPosition = new Vector2(_targetRectTransform.anchoredPosition.x, _initialYPosition - _currentYPosition);
                yield return new WaitForSeconds(delay);
            }

            _currentYPosition += _scrollSpeed * Time.deltaTime * _direction;
            _targetRectTransform.anchoredPosition = new Vector2(_targetRectTransform.anchoredPosition.x, _initialYPosition - _currentYPosition);

            yield return null;
        }
    }
}
