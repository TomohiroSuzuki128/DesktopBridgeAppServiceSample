# DesktopBridgeAppServiceSample

# 動作環境
- Visual Studio 2017 or 2019
- Windows 10 (1803以上)


## Visual Studio では以下を必ずインストールしてください。
- C++ によるデスクトップ開発　
 - Windows 10 SDK (10.0.17134.0)
 - Windows 10 SDK (10.0.16299.0)
 - Windows 10 SDK (10.0.15063.0)
 - Windows 10 SDK (10.0.14393.0)
 - Windows 10 SDK (10.0.10586.0)
 - Windows 10 SDK (10.0.10240.0)


# サンプルアプリの概要

UWP (Xamarin.Forms)アプリと WPF アプリを Desktop Bridge で1つのアプリにパッケージングし、UWP (Xamarin.Forms)アプリと WPF アプリが互いに通信を行ってデータのやり取りができるサンプルです。

既存資産の WPF アプリを少改良で流用し、新規作成の部分のみ UWP で作成し、徐々に UWP に移行したいときの過渡期に使用するシナリオのために作成したサンプルです。

ユーザは、内部的に2つのアプリであることを意識せず、まるで1つのアプリのように使用できます。


# このサンプルアプリでできること

- UWP (Xamarin.Forms)アプリから WPF アプリにデータを送信する。
- WPF アプリから UWP (Xamarin.Forms) アプリにデータを送信する。
- WPF アプリから UWP (Xamarin.Forms) アプリの画面遷移を操作する。


# ソリューション内のプロジェクトの説明

## LaunchApp
Desktop Bridge のパッケージプロジェクト
 
## Tools 
dll が入っているフォルダ
 
## UwpXamFormsApp.UWP 
UWP アプリのエントリポイント
 
## UwpXamFormsApp 
UWP アプリの Xamarin.Forms によるアプリ本体

## WpfApp 
Wpf アプリ


# TIPS

基本的には、アプリ内部の操作を インターフェースを使って外側に引っ張り出して、アプリ サービスから叩けるようにしているだけです。よって、同じ方法で、データ送信や画面遷移以外でも、そのアプリでできる操作はほとんど何でもできます。

アプリサービスを通したコマンドのやり取りは、サンプルのため文字列で行っていますが、プロダクトコードでは、きちんとコマンドのプロトコルを作成し、シリアライズしたデータをアプリ サービスでやりとりしたほうがよいでしょう。
