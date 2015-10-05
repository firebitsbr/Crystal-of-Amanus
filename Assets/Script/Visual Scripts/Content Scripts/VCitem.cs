﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VCitem : Selectable
{
    //Como a imagem do item vai estar por trás da imagem de seleção, vou ter que pegar a imagem do item pelo pai.
    public Image ImgPai;
    public Image Img_Item;
    //Nesse Text eu vou escrever o nome e a quantidade do item
    public Text Txt;
    //Aqui eu vou escrever a descrição do item.
    Text Descricao;
    //O personagem é o alvo do item
    Character Personagem;
    //O inventário é aonde eu vou pegar as informações.
    Inventario inv;
    //O indice vai me dizer exatamente qual item eu estou
    public int Indice = -1;
    string texto;
    void Start()
    {
        if (!Application.isPlaying) return;
        Personagem = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        Img_Item = ImgPai.transform.FindChild("Item_Img").GetComponent<Image>();
        inv = Personagem.GetComponent<Inventario>();
        //Content > Area com mascara > Area >Box > Area_Inventario
        Descricao = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.Find("Descricao").GetComponent<Text>();
        texto = "Descrição:\n\t" + inv.ItemInformations(Indice).Descricao;
    }

    void Update()
    {
        if (!Application.isPlaying) return;
        if (Indice != -1)
        {
            if (inv != null)
            {
                Img_Item.sprite = inv.ItemInformations(Indice).Img;
                Txt.text = inv.ItemInformations(Indice).Nome + " X" + inv.QuantityInformations(Indice);
            }else
            {
                Debug.LogError("Inventário é nulo, você esqueceu de atribuir valor");
            }
        }else
        {
            if (Descricao.text == texto)
            {
                Descricao.text = "Descrição:\n\t";
            }
            Destroy(gameObject);
        }
    }

    public void MouseDown()
    {
        var i = inv.ItemInformations(Indice).Tipo;
        
        if (!inv.UsarItem(inv.ItemInformations(Indice), Personagem))
        {

            Indice = -1;
        }
        if (i == Item.TipoDeItem.Equipamento)
        {

            GameObject.Find("Canvas").transform.FindChild("Menu").transform.FindChild("Area_Informacao").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.FindChild("Menu").transform.FindChild("Area_Habilidades").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.FindChild("Menu").transform.FindChild("Area_Inventario").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.FindChild("Menu").transform.FindChild("Area_Quest").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.FindChild("Menu").transform.FindChild("Area_Mapa").gameObject.SetActive(false);

        }
        
    }
    public void MouseOver()
    {
        Descricao.text = texto;
    }
}