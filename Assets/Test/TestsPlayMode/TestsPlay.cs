using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class TestsPlay
{

    [SetUp]
    public void Setup()
    {
        EditorSceneManager.LoadScene("Main");
    }

    [UnityTest]
    public IEnumerator TestsPlayWithEnumeratorPasses()
    {
        GameObject spriteDonKeyKongfree = GameObject.Find("donkeykongfree");
        GameObject spriteDonKeyKongwinner = GameObject.Find("donkeykongfreewinner");
        spriteDonKeyKongfree.GetComponent<SpriteRenderer>().enabled = false;
        spriteDonKeyKongwinner.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1.5f);
        spriteDonKeyKongfree.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(1.5f);
        spriteDonKeyKongfree.GetComponent<SpriteRenderer>().enabled = false;
        spriteDonKeyKongwinner.GetComponent<SpriteRenderer>().enabled = true;

        yield return null;
    }

    [TearDown]
    public void TearDown()
    {
        EditorSceneManager.UnloadSceneAsync("Main");
    }
}
