using System;
using System.Collections.Generic;
using Controllers;
using Data;
using SaveSystem;
using UnityEngine;

public class GameStarter : MonoBehaviour {
	public BusinessConfigData   BusinessConfig;
	public BusinessLocalsData   BusinessLocals;
	public BusinessViewsManager BusinessViewsManager;

	BusinessInfoProvider       _businessInfoProvider;
	BusinessEntitiesController _businessEntitiesController;
	BalanceController          _balanceController;
	LocaleController              _localeController;
	SaveСontroller             _saveСontroller;

	GameData _data;

	HashSet<BaseController> _controllers = new HashSet<BaseController>();

	void Start() {
		//Инициализация контроллеров и инфо-провайдеров
		_businessEntitiesController = new BusinessEntitiesController(BusinessConfig);
		_balanceController          = new BalanceController();
		_localeController           = new LocaleController(BusinessLocals);
		FillControllersList();

		_saveСontroller       = new SaveСontroller(this);
		_businessInfoProvider = new BusinessInfoProvider(BusinessConfig, _businessEntitiesController, _balanceController);

		BusinessViewsManager.Init(_businessInfoProvider, _balanceController, _localeController);

		// Добавление бизнесов
		AddBusinesses();

		_saveСontroller.Load();
	}

	void Update() {
		_businessEntitiesController.UpdateEntitiesTick(Time.deltaTime);
	}

	void OnDestroy() {
		_businessEntitiesController.Deinit();
	}

	void SetupDevice() {
		Application.backgroundLoadingPriority = ThreadPriority.Normal;
		Screen.sleepTimeout                   = SleepTimeout.NeverSleep;
		Application.targetFrameRate           = 60;
		// Hack: for some reason beyond comprehension, sometimes unity reports 60 as 59, thus this +2
		QualitySettings.vSyncCount = (Screen.currentResolution.refreshRate + 2) / 60;
		Input.multiTouchEnabled    = false;
	}

	public void AddBusiness() {
		var newEntity = _businessEntitiesController.TryAddBusinessEntity();
		if ( newEntity != null ) {
			BusinessViewsManager.AddBusinessView(newEntity);
		}
	}

	void OnApplicationQuit() {
		_saveСontroller.Save();
	}

	void OnApplicationPause(bool pauseStatus) {
		if ( Application.platform == RuntimePlatform.Android ) {
			_saveСontroller.Save();
		}
	}

	public List<ISavableEntity> GetSavableControllers() {
		var savablesList = new List<ISavableEntity>();
		foreach ( var controller in _controllers ) {
			if ( controller is ISavableEntity savableEntity ) {
				savablesList.Add(savableEntity);
			}
		}
		return savablesList;
	}

	void FillControllersList() {
		var controllersSet = new HashSet<BaseController>(BaseController.Instances);
		foreach ( var controller in controllersSet ) {
			try {
				_controllers.Add(controller);
			} catch ( Exception e ) {
				Debug.LogException(e);
			}
		}
	}

	void AddBusinesses() {
		for ( var i = 0; i < BusinessConfig.AllBusinesses.Count; i++ ) {
			AddBusiness();
		}
		_businessEntitiesController.UpgradeBusinessByIndex(0);
	}
}