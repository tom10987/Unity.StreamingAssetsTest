
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StreamingAssetsTest : MonoBehaviour
{
  [SerializeField]
  Text _text = null;

  IEnumerator Start()
  {
    // ローカルフォルダにあるデータを読み込むには、file:// をパスの先頭につける
    var path = "file://" + Application.streamingAssetsPath + "/test.txt";
    Debug.Log(path);

    // 指定したパスにあるデータを読み込む
    // 本来はインターネット上にあるデータを取得するものだが、
    // ローカルフォルダを指定することもできる
    WWW www = new WWW(path);

    if (!string.IsNullOrEmpty(www.error))
    {
      Debug.LogError(www.error);
      yield break;
    }

    // 読み込み完了まで待機
    yield return www;

    _text.text = www.text;
    Debug.Log("finish");
  }
}
