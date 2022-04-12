# BatailleNavale
Jeu de bataille navale

On considère 5 types de navires:
- porte avion long=5
- croiseur long=4
- frégate long=3
- sous-marin long=3
- escorteur long=2 (x2)

La grille fait 8x8.

Chaque navire est positionné par un point de départ D(x,y) et un point d'arrivée A(x,y).

Le joueur distant est identifié par un nom de machine sur le réseau.
A l'initialisation, le premier joueur envoyant une commande donne automatiquement la main (jeton) à l'autre joueur.

Fonctionnement:
LE joueur débute la partie 
La liaison doit être vérifiée avant d'accepter l'envoi de chaque 'coup'.
Un coup doit être vérifié avant d'être émis:
- pas de rejeu (en option, on peut ultérieurement accepter un coup déjà joué)
- dans la grille (0<x<9, 0<y<9)
Un message retour informe le joueur que la commande est bien passée et précise le résultat du coup:
- dans l'eau
- touché
-coulé (avec le type de bâteau)
- fin de partie (tous bâteaux coulés)

Structure:
* classe connexion
Gère l'état de la liaison:
- nom destinataire
- état du lien réseau (disponible ou cassé)
- état du serveur distant (répond ou pas - plusieures tentatives avant de conclure)
- opération en cours (établissement du lien, vérification du lien, idem pour le serveur distant)
- produire une synthèse utilisée par l'objet Affichage.
- produire un warning à usage de l'objet Action.
- indication à disposition du joueur distant que notre grille est disponible 'ready'

* classe grille
Gère l'état du jeu:
- initialisation des grilles joueur1 et joueur2
	. interface utilisateur (saisie des positions des bâteaux via leurs identifiants)
	. respect des règles (position, dimension, superposition)
- établissement d'un coup (client)
	. saisie et contrôle de sa validité
	. attendre la confirmation de sa prise en compte
	. affichage et mise à jour
	(après définition du coup, après réception du coup adversaire:
	si impact, placer T dans la case ou C dans chaque case du bâteau)
- réception d'un coup (serveur)
	. analyse du coup et impact (dans l'eau, touché, coulé)
	. envoi du message impact vers l'adversaire
	. affichage grille et effet constaté (log ou popup)'

* classe bâteau
Gère l'état d'un bâteau
- identifiant (List)
- longueur
- positions mémorisées ? (peut-être que l'info dans la grille est suffisant)
- état pour chaque position (intact, touché)
- état global (coulé: compteur = longueur puis décrémente si touché)
