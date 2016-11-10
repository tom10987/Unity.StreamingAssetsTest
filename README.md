
## Unity で外部ファイルを読み込む方法のサンプルです

---

#### はじめに

`Unity` には、エディターによる読み取りが行われる特殊なフォルダ名があります。

* `Assets`  
フォルダの内容が、エディターの `Project` ビューに表示されます。

* `Editor`  
エディターそのものに、機能を追加したりするためのスクリプトを含めるためのフォルダです。  
このフォルダにあるスクリプトは、ゲームのスクリプトとして使うことができません。

* `Resources`  
`Resources.Load()` メソッドで、このフォルダ内にあるアセットを読み込めます。

* `Standard Assets`  
`Unity` が標準で提供しているアセットパッケージを利用するときに作成されます。  
スクリプトのコンパイル順番が変わるため、自分のスクリプトは入れないほうがいいです。

* `StreamingsAssets`  
`Assets` フォルダ内にある、このフォルダに入っているアセットは、ビルド時に変換されずにコピーされます。  
アプリ実行中に外部ファイルを読み込みたい場合に利用することができます。  
**ただし、** `Resources.Load()` **による読み込みができません。**

---

本来であれば、専用のエディターを別に用意して、それを使って編集するのが望ましいです。  
ただし、そこまで出来ない場合は、アプリの実行時にデータを読み込んで確認するという方法もあります。

`Unity` の場合、外部ファイルを読み込む方法には、下記の方法があります。

* `C#` のファイル操作機能を使う
* `Unity` が持つ `WWW` クラスを使う

このサンプルでは、`WWW` クラスによる外部ファイル読み込みを使っています。

---

#### 使い方

~~~C#

using UnityEngine;
using UnityEngine.UI;
using System.Collection;

public class StreamingAssetsTest : MonoBehaviour
{
  // サンプルでは、Unity の Canvas を使用している
  public Text text = null;

  // WWW クラスによる読み込みは、コルーチンを使うのが望ましい
  IEnumerator Start()
  {
    // Text コンポーネントを取得
    // インスペクターからコンポーネントを設定する場合は、この処理は必要ありません
    text = GetComponent<Text>();

    /* WWW クラスでローカル (PC など) にあるデータを読み込むには、最初に file:// をつけます
     * streamingAssetsPath は、StreamingAssets フォルダまでのパスを返します
     * ただし、フォルダを示す / がないため、読み込みたいファイル名の最初に / をつけます
     */
    string path = "file://" + Application.streamingAssetsPath + "/test.txt";

    // ファイルパスを渡して読み込み開始
    WWW www = new WWW(path);
  
    /* 読み込みに失敗したときのエラーチェックなどを行う */

    // ファイル読み込みが完了するまで処理を中断
    yield return www;

    // 読み込んだテキストを画面に表示します
    text.text = www.text;
  }
}

~~~
