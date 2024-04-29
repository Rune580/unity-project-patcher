﻿using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Nomnom.UnityProjectPatcher.Editor.Steps {
    public readonly struct OpenSceneStep: IPatcherStep {
        private readonly string _sceneName;
        
        public OpenSceneStep(string sceneName) {
            _sceneName = sceneName;
        }
        
        public UniTask<StepResult> Run() {
            var sceneName = _sceneName;
            var initScene = EditorBuildSettings.scenes.FirstOrDefault(scene => scene.path.Contains(sceneName));
            if (initScene == null) {
                Debug.LogError($"Could not find scene with name \"{_sceneName}\"");
                return UniTask.FromResult(StepResult.Success);
            }
            
            EditorSceneManager.OpenScene(initScene.path);
            return UniTask.FromResult(StepResult.Success);
        }
    }
}