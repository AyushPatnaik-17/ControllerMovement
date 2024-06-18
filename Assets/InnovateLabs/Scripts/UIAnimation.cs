using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    //[SerializeField] private Toggle _handToggle;
    //[SerializeField] private RectTransform _subMenuBackground;
    //[SerializeField] private RectTransform _dropDownElement;
    //[SerializeField] private RectTransform _measurementElement;
    //[SerializeField] private RectTransform _trackpadElement;
    //private float _subMenuDefaultHeight;
    //private float _elementsDefaulPosY;

    //public float subMenuTweenDuration = 0.3f;
    //public float menuElementsDurationOffset = 0.1f;
    //public float subMenuRequiredHeight = 131f;
    //public float dropDownRequiredPosY = 131f;
    //public float measurementRequiredPosY = 131f;
    //public float trackpadRequiredPosY = 131f;
    


    //public void Awake()
    //{
    //    _handToggle.onValueChanged.AddListener(TweenUIElements);
    //    _subMenuDefaultHeight = _subMenuBackground.sizeDelta.y;
    //    _elementsDefaulPosY = _dropDownElement.anchoredPosition.y;

    //}

    //private void TweenUIElements(bool isToggled)
    //{
    //    if (isToggled)
    //    {
    //        _subMenuBackground.DOSizeDelta(new Vector2(_subMenuBackground.sizeDelta.x, subMenuRequiredHeight), subMenuTweenDuration);
    //        _dropDownElement.DOAnchorPosY(dropDownRequiredPosY, subMenuTweenDuration + 0.09f);
    //        //_measurementElement.DOAnchorPosY(measurementRequiredPosY, subMenuTweenDuration + 0.09f);
    //        //_trackpadElement.DOAnchorPosY(trackpadRequiredPosY, subMenuTweenDuration + 0.09f);
    //        _trackpadElement.DOAnchorPosY(measurementRequiredPosY, subMenuTweenDuration + 0.09f);
    //        // this is temporary for as long as the measurement is not added, I have just put down the position where the measurement is supposed to be
            
    //    }
    //    else
    //    {
    //        _subMenuBackground.DOSizeDelta(new Vector2(_subMenuBackground.sizeDelta.x, _subMenuDefaultHeight), subMenuTweenDuration);
    //        _dropDownElement.DOAnchorPosY(_elementsDefaulPosY, subMenuTweenDuration - menuElementsDurationOffset);
    //        //_measurementElement.DOAnchorPosY(_elementsDefaulPosY, subMenuTweenDuration - menuElementsDurationOffset);
    //        _trackpadElement.DOAnchorPosY(_elementsDefaulPosY, subMenuTweenDuration - menuElementsDurationOffset);
    //    }
    //}

    //public void OnDestroy() => _handToggle.onValueChanged.RemoveListener(TweenUIElements);


}
