using UnityEngine;
using CosmicCuration.Player;
using System.Threading.Tasks;
using System.Collections;

namespace CosmicCuration.PowerUps
{
    public class PowerUpController : IPowerUp
    {
        private PowerUpView powerUpView;
        private float activeDuration;
        private bool isActive;

        private Coroutine timerRoutine;

        public PowerUpController(PowerUpData powerUpData)
        {
            powerUpView = Object.Instantiate(powerUpData.powerUpPrefab);
            powerUpView.SetController(this);
            activeDuration = powerUpData.activeDuration;
        }

        public void Configure(Vector2 spawnPosition)
        {
            isActive = false;
            powerUpView.transform.position = spawnPosition;
            powerUpView.gameObject.SetActive(true);
        }

        //public async void StartTimer()
        //{
        //    if (isActive)
        //    {
        //        await Task.Delay(Mathf.RoundToInt(activeDuration * 1000));
        //        Deactivate();
        //    }
        //}
        private void StartTimer()
        {
            // Stop previous timer (important for pooling)
            if (timerRoutine != null)
                CoroutineRunner.Instance.StopCoroutine(timerRoutine);

            timerRoutine = CoroutineRunner.Instance.StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            yield return new WaitForSecondsRealtime(activeDuration);

            if (isActive)
                Deactivate();
        }
        public void PowerUpTriggerEntered(GameObject collidedObject)
        {
            if (collidedObject.GetComponent<PlayerView>() != null)
                Activate();
        }

        public virtual void Activate()
        {
            if (isActive) return;

            isActive = true;

            powerUpView.gameObject.SetActive(false);

            StartTimer();
        }

        //public virtual void Deactivate()
        //{
        //    isActive = false;
        //    GameService.Instance.GetPowerUpService().ReturnPowerUpToPool(this);
        //}
        public virtual void Deactivate()
        {
            if (!isActive) return;

            isActive = false;

            // Stop timer (extra safety)
            if (timerRoutine != null)
            {
                CoroutineRunner.Instance.StopCoroutine(timerRoutine);
                timerRoutine = null;
            }

            GameService.Instance.GetPowerUpService().ReturnPowerUpToPool(this);
        }
    } 
}