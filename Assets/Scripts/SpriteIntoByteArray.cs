using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

    [Serializable]
    public class SerializeTexture
    {
        [SerializeField]
        public int x;
        [SerializeField]
        public int y;
        [SerializeField]
        public byte[] bytes;
    }
public static class SpriteToSerialize
{
    public static SerializeTexture SpriteToSTexture(Sprite inp)
    {
        if (inp == null)
            return null;
        SerializeTexture sTexture = new SerializeTexture();
        sTexture.x = inp.texture.width;
        sTexture.y = inp.texture.height;
        sTexture.bytes = ImageConversion.EncodeToPNG(inp.texture);
        return sTexture;
    }
    public static Sprite STextureToSprite(SerializeTexture sTexture)
    {
        if (sTexture == null)
            return Sprite.Create(null, new Rect(0.0f, 0.0f, 0, 0), Vector2.one);
        Texture2D tex = new Texture2D(sTexture.x, sTexture.y);
        ImageConversion.LoadImage(tex, sTexture.bytes);
        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
    }
    public static SerializeTexture PathToSTexture(string path)
    {
        if (path == "")
            return new SerializeTexture();
        SerializeTexture sTexture = new SerializeTexture();
        byte[] byteArray = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        sTexture.x = tex.width;
        sTexture.y = tex.height;
        sTexture.bytes = byteArray;
        return sTexture;
    }
    public static Sprite PathToSprite(string path)
    {
        if(path == "")
            return Sprite.Create(null, new Rect(0.0f, 0.0f, 0, 0), Vector2.one);
        byte[] byteArray = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        ImageConversion.LoadImage(tex, byteArray);
        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
    }
}
