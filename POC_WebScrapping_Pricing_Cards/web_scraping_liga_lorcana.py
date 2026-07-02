import requests
from bs4 import BeautifulSoup

URL = "https://www.ligalorcana.com.br/ajax/cards/main.php"

payload = {
    "opc": "nextPage",
    "page": "2", # Página que você quer buscar, podem ser alteradas aqui
    "totalReg": "0",
    "search": "precoate=1.000.000,00 precotipo=1 searchprod=0",
    "orderBy": "",
    "tipo": "1",
    "fav": "false",
    "iTCG": "9",
    "idPokemon": "0",
    "key": "160"
}

headers = {
    "User-Agent": "Mozilla/5.0",
    "X-Requested-With": "XMLHttpRequest",
    "Referer": "https://www.ligalorcana.com.br/?view=cards/search",
    "Origin": "https://www.ligalorcana.com.br"
}

response = requests.post(URL, data=payload, headers=headers)

print("Status:", response.status_code)

# Caso dê erro já conseguimos ver o retorno
if response.status_code != 200:
    print(response.text)
    exit()

try:
    data = response.json()
except Exception:
    print("A resposta não é um JSON.")
    print(response.text[:1000])
    exit()

html = data["html"]

soup = BeautifulSoup(html, "html.parser")

cards = soup.select(".mtg-single")

print(f"Foram encontradas {len(cards)} cartas.\n")

for card in cards:
    try:
        nome = card.select_one(".mtg-name").get_text(strip=True)
        preco_min = card.select_one(".price-min").get_text(strip=True)
        preco_med = card.select_one(".price-avg").get_text(" ", strip=True)
        preco_max = card.select_one(".price-max").get_text(strip=True)

        print(f"{nome} | {preco_min} | {preco_med} | {preco_max}")

    except Exception as ex:
        print("Erro ao ler uma carta:", ex)