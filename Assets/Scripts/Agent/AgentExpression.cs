using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agent
{
    public class AgentExpression : MonoBehaviour
    {
        [SerializeField] private Image theExpression;
        //[SerializeField] private List<Sprite> expressionSprites;
        private Coroutine _theCoroutine;
        private Animator TheAnimator => GetComponent<Animator>();

        public void ChangeExpression(string expression)
        {
            if(_theCoroutine != null)StopCoroutine(_theCoroutine);

            
            switch (expression)
            {
                case "平常":
                    //theExpression.sprite = expressionSprites[0];
                    TheAnimator.Play("Common");
                    break;
                case "开心":
                    //theExpression.sprite = expressionSprites[1];
                    TheAnimator.Play("Happy");
                    break;
                case "生气":
                    //theExpression.sprite = expressionSprites[2];
                    TheAnimator.Play("Angry");
                    break;
                case "疑惑":
                    //theExpression.sprite = expressionSprites[3];
                    TheAnimator.Play("Confused");
                    break;
                default:
                    TheAnimator.Play("Normal");
                    break;
            }
            
            _theCoroutine = StartCoroutine(ReserveToNormal());
        }

        private IEnumerator ReserveToNormal()
        {
            yield return new WaitForSeconds(5);
            TheAnimator.Play("Normal");
        }
    }
}