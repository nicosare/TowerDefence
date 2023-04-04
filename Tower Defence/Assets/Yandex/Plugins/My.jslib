mergeInto(LibraryManager.library, {

  RateGame: function () {
    ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  },

  SaveExtern: function(date) {
    var dataString = UTF8ToString(date);
    var myobj = JSON.parse(dataString);
    player.setData(myobj);
  },

  LoadExtern: function(date) {
    player.getData().then(_date =>{
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
    });
  },

  GetLang: function() {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
  },

  ShowAdv : function() {
    ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
    })
  },
  
  AddUnitExtern : function() {
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
          myGameInstance.SendMessage("ButtonsManager", "PauseGame");
          myGameInstance.SendMessage("BackgroundMusic", "SoundOff");
        },
        onRewarded: () => {
          console.log('Rewarded!');
        },
        onClose: () => {
          console.log('Video ad closed.');
          myGameInstance.SendMessage("ButtonsManager", "PlayGame");
          myGameInstance.SendMessage("BackgroundMusic", "SoundOn");
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
  })
  },

  BuyFraction : function(){
    payments.purchase({ id: 'fraction' }).then(purchase => {
      // Покупка успешно совершена!
      myGameInstance.SendMessage("InAPP", "OpenChooseNextFraction");
    }).catch(err => {
      // Покупка не удалась: в консоли разработчика не добавлен товар с таким id,
      // пользователь не авторизовался, передумал и закрыл окно оплаты,
      // истекло отведенное на покупку время, не хватило денег и т. д.
    })
  },

});