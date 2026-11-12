using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CMP5402.Week8Lab
{
    public sealed class Week8MonetisationLab : MonoBehaviour
    {
        public enum AdOutcome { Unavailable, Cancelled, Completed }

        [Serializable]
        private sealed class TestLedger
        {
            public int coins;
            public bool rewardClaimed;
            public List<string> orders = new List<string>();
        }

        [SerializeField] private TMP_Text status;
        [SerializeField] private Button rewardButton;
        [SerializeField] private Button purchaseButton;
        [SerializeField] private string testProfile = "run-1";
        [SerializeField] private AdOutcome adOutcome = AdOutcome.Unavailable;
        [SerializeField] private bool shopAvailable = true;
        [SerializeField] private bool simulateSaveFailure;
        [SerializeField] private string testOrderId = "order-a";

        private TestLedger ledger;
        private string storageKey;
        private bool ready;

        private void Awake()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            storageKey = "CMP5402.Week8Lab." + testProfile;
            try
            {
                ledger = PlayerPrefs.HasKey(storageKey)
                    ? JsonUtility.FromJson<TestLedger>(PlayerPrefs.GetString(storageKey))
                    : new TestLedger();
                ready = ledger != null && ledger.orders != null;
                Display(ready ? "Choose an optional test offer." : "Test data unavailable.");
            }
            catch (Exception)
            {
                Display("Test data unavailable. Use a new Test Profile.");
            }
#else
            gameObject.SetActive(false);
#endif
        }

        private void LateUpdate()
        {
            if (rewardButton != null)
                rewardButton.interactable = ready && !ledger.rewardClaimed
                    && adOutcome != AdOutcome.Unavailable;
            if (purchaseButton != null)
                purchaseButton.interactable = ready && shopAvailable;
        }

        public void TryTestReward()
        {
            if (!ready || ledger.rewardClaimed || adOutcome == AdOutcome.Unavailable)
                return;
            if (adOutcome == AdOutcome.Cancelled)
            {
                Display("Test ad cancelled. No reward.");
                return;
            }

            var next = CopyLedger();
            next.coins += 10;
            next.rewardClaimed = true;
            Display(Store(next) ? "Test ad completed: +10 lab coins." : "Test save failed. No reward.");
        }

        public void TryTestPurchase()
        {
            if (!ready || !shopAvailable) return;
            string id = testOrderId?.Trim();
            if (string.IsNullOrEmpty(id))
            {
                Display("Set a Test Order Id.");
                return;
            }
            if (ledger.orders.Contains(id))
            {
                Display("Duplicate order " + id + ". No additional coins.");
                return;
            }

            var next = CopyLedger();
            next.coins += 100;
            next.orders.Add(id);
            Display(Store(next)
                ? "Test order " + id + " saved. Simulated acknowledgement."
                : "Test save failed. Order not acknowledged. Try the same ID again.");
        }

        private TestLedger CopyLedger()
        {
            return new TestLedger
            {
                coins = ledger.coins,
                rewardClaimed = ledger.rewardClaimed,
                orders = new List<string>(ledger.orders)
            };
        }

        private bool Store(TestLedger next)
        {
            if (simulateSaveFailure) return false;
            PlayerPrefs.SetString(storageKey, JsonUtility.ToJson(next));
            PlayerPrefs.Save();
            ledger = next;
            return true;
        }

        private void Display(string message)
        {
            if (status != null)
                status.text = "TEST ONLY - No real ads or payments\nLab coins: "
                    + (ledger?.coins ?? 0) + "\n" + message;
        }
    }
}