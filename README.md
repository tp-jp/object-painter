# ObjectPainter

メッシュの法線に沿ってオブジェクトを配置できるツールです。

## 導入方法

VCCをインストール済みの場合、以下の**どちらか一つ**の手順を行うことでインポートできます。

- [https://tp-jp.github.io/vpm-repos/](https://tp-jp.github.io/vpm-repos/) へアクセスし、「Add to VCC」をクリック

- VCCのウィンドウで `Setting - Packages - Add Repository` の順に開き、 `https://tp-jp.github.io/vpm-repos/index.json` を追加

[VPM CLI](https://vcc.docs.vrchat.com/vpm/cli/) を使用してインストールする場合、コマンドラインを開き以下のコマンドを入力してください。

```
vpm add repo https://tp-jp.github.io/vpm-repos/index.json
```

VCCから任意のプロジェクトを選択し、「Manage Project」から「Manage Packages」を開きます。
一覧の中から `MaterialBuilder` の右にある「＋」ボタンをクリックするか「Installed Vection」から任意のバージョンを選択することで、プロジェクトにインポートします。
![image](https://github.com/user-attachments/assets/1aed6aed-c56d-43a8-8e92-4100583ba43f)

リポジトリを使わずに導入したい場合は [releases](https://github.com/tp-jp/object-painter/releases) から unitypackage をダウンロードして、プロジェクトにインポートしてください。


## VPMを使わずにPackageManagerから導入したい場合

- ツールバーから `Window>Package Manager` を選択。
- 左上の「＋」ボタンをクリックし、 `Add package from git URL` を選択。
- URLに https://github.com/tp-jp/object-painter.git?path=Packages/com.tp-lab.object-painter を入力して「Add」ボタンを選択。


## 使い方


## 更新履歴

[CHANGELOG](CHANGELOG.md)

