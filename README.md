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

## Install manually (UPM)

以下を UPM でインストールしてください。
```
https://github.com/tp-jp/object-painter.git?path=Packages/com.tp-lab.object-painter
```

## VPMを使わずにPackageManagerから導入したい場合

- ツールバーから `Window>Package Manager` を選択。
- 左上の「＋」ボタンをクリックし、 `Add package from git URL` を選択。
- URLに https://github.com/tp-jp/object-painter.git?path=Packages/com.tp-lab.object-painter を入力して「Add」ボタンを選択。


## 使い方

1. ツールバーから `TpLab>ObjectPainter` を選択します。

2. 表示されたウィンドウの設定を行い、ObjectPaintを実施します。

   - Target Mesh     
     オブジェクトを配置する対象メッシュを指定します。

   - Place Object  
     配置するオブジェクトを指定します。

   - Parent Object  
     配置したオブジェクトの親を指定します。
     
   - Brush Size  
     オブジェクトを配置するブラシのサイズを指定します。
     
   - Density  
     オブジェクトを配置する密度を指定します。

   - Placement Settings  
     配置設定を指定します。

     - Rotation Range X/Y/Z  
       配置したオブジェクトの回転範囲を指定します。

     - Scale Range  
       配置したオブジェクトのスケール範囲を指定します。

   - Limit Settings  
    範囲の制限値を指定します。

     - Brush Size Limit  
       ブラシサイズの制限値を指定します。

     - Density Limit  
       密度の制限値を指定します。

     - Rotation Min Limit X/Y/Z  
       各回転軸範囲の最小値を指定します。

     - Rotation Max Limit X/Y/Z  
       各回転軸範囲の最大値を指定します。

     - Scale Min Limit  
       スケール範囲の最小値を指定します。

     - Scale Max Limit  
       スケール範囲の最大値を指定します。

   - Start Painting
     オブジェクトの配置を開始します。
     開始後に対象メッシュ上で左クリックを押すとオブジェクトを配置できます。

## 更新履歴

[CHANGELOG](CHANGELOG.md)

