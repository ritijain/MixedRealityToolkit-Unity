// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos.EyeTracking
{
    /// <summary>
    /// When the button is selected, it triggers starting the specified scene.
    /// </summary>
    public class LoadAdditiveScene : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Timeout in seconds before new scene is loaded.")]
        private float waitTimeInSecBeforeLoading = 0.25f;

        public void LoadScene(string sceneName)
        {
            if (!string.IsNullOrWhiteSpace(sceneName))
            {
                StartCoroutine(LoadNewScene(sceneName));
            }
            else
            {
                Debug.Log($"Unsupported scene name: {sceneName}");
            }
        }

        private IEnumerator LoadNewScene(string sceneName)
        {
            // Let's find out the name of the currently loaded additive scene to unload
            if (SceneManager.sceneCount > 1)
            {
                string lastSceneLoaded = SceneManager.GetSceneAt(1).name;

                Debug.Log($"Last scene name: {lastSceneLoaded}");

                // Let's wait in case we don't want to switch scenes too abruptly 
                yield return new WaitForSeconds(waitTimeInSecBeforeLoading);

                SceneManager.UnloadSceneAsync(lastSceneLoaded);
            }

            Debug.Log($"New scene name: {sceneName}");

            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
}