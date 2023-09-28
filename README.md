## 概要
制作にあたってのお題「交わる」をテーマとして、約一ヵ月の期間でデザインパターンの練習として制作した作品です。

## タイトル
「Intersect」
は(道路などと)交差する、交わる (地域などを)横切る、横断するの意味なので、このタイトルの意味から発想して、鉄道の交差するところの転車台をグルグル回して遊べることをRPしたいです。

## テーマ「交わる」を表現した点
・「交わる」をイメージしてから最初は普通な交差点を考えましたが、普通な交差点はやはり普通過ぎと判断されますので、鉄道の方の交差点はどうなりますかと調べたら、転車台の動画を流れてきた、以外に面白そうなので、転車台を操作して多線路からの列車を正しい他線路へ移動するゲームを企画しました。

## 適用したデザインパターン
・Singleton

サンプルコードは
```
public static AudioController Instance { get { return instance; } set { instance = value; } }
//...
private void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(gameObject);
        Debug.LogError("AudioControllerのインスタンスが失敗しました。");
    }
}
```
のようになります。

利用サンプルは
```
AudioController.Instance.メンバー
```
のようになります。

今の段階はinstanceのクラスのようなものを作ってありません。

・Abstract factory
https://github.com/kannkeii/Intersect/tree/master/Assets/Scripts/TrainFactory

利用サンプルは
```
https://github.com/kannkeii/Intersect/blob/master/Assets/Scripts/GenerateTrack.cs
private void Create(int enterRoadCnt, int exitRoadCnt, int trainCnt)
{
    TrainFactory trainFactory = new DieselTrainFactory();
    GameObject dieselTrain = trainFactory.CreateTrain(trainPrefab);
}
```
のようになります。
初版の列車も、鉄道もただ一種類であります。これから増やす場合は活用できるでしょう。

・その他  
今回の制作はイベント(delegate,action,event,func)を大量使っています。
個人的にまとめた各の関係は
```
                     →略書き→action→略書き→func
delegate(オリジナル)→|
                     →権限制限→event
```

利用イメージは  
-delegate、実行する時、戻り値がない場合
```
public delegate void GameStartHandler();
public event GameStartHandler OnGameStart;
OnGameStart += Turntable.Instance.GenerateLevel1;//実行したいメンバー追加
OnGameStart?.Invoke();//追加されたメンバーを全部実行
OnGameStart -= Turntable.Instance.GenerateLevel1;//実行したいメンバー削減
OnGameStart = null;//追加されたメンバーを全部削減
```

-delegate2、実行する時、戻り値がある場合
```
public delegate void RoadStatusChangedHandler(string roadName, string exitName, bool isCanPass);
public event RoadStatusChangedHandler OnRoadStatusChanged;
OnRoadStatusChanged?.Invoke(com.name, default, false);
```

-action
```
public static event Action OnCountdownFinished;
OnCountdownFinished += () =>//実行したいメンバーを追加
{
    AudioController.Instance.PlayMusic(1);
    OnGameStart?.Invoke();
};

OnCountdownFinished?.Invoke();//追加されたメンバーを全部実行
```

今回の制作はメッセージシステム(SendMessage)を少し使っています。  
利用イメージは
-送信側
```
GameObjcet taget
taget.SendMessage("受ける側関数名","送信内容", //送信内容の部分は複数種類な変数が入れられます。
                  SendMessageOptions.DontRequireReceiver);//受ける側が見つからない場合のエラーメッセージはなしにします。
```

## ゲームの説明
ゲーム開始後ランタイムで鉄道を複数生成し、ユーザーはキーボードの左右矢印ボタンを押して画面真ん中の転車台を操作し、鉄道に赤い矢印を沿ってくる列車を誘導して緑矢印がある鉄道へ移動するゲームです。

## 操作方法
・転車台の回転　　　キーボードの左右矢印

## 仕様書
https://github.com/kannkeii/Intersect/blob/master/%E4%BB%95%E6%A7%98%E6%9B%B8.xlsx

## 予定
以下のステップで進むつもりです。
①フールデバッグします。
②ソースコードをきれいに調整します。
②遊び要素を増やします。
③スマホに対応して、プレイストアに提出します。
④PICO4 VR、ARHMDに対応して、PICOストアへ提出します。
