Inventory System Overview

Design Patterns Implemented:

MVC (Model-View-Controller): Clean separation between data (Models), game logic (Controllers), and UI (Views) for both Player and Shop systems.

Singleton Pattern: A Centralized Pattern for GameService and EventService.

Observer Pattern: A centralized EventService manages reactive UI updates by broadcasting events on model changes.

Player System:

PlayerModel maintains the player's coin count, inventory list, and carry weight.

PlayerController manages core logic for buying/selling items, validates conditions (e.g. weight limits, coin balance), and updates the model accordingly.

Shop System:

ShopModel contains the shop’s inventory.

ShopController handles item exchanges between the shop and the player.

UI System (UIView):

Listens to model events via EventService to ensure real-time updates.

Dynamically displays player/shop inventory, coin count, item details, and carry weight.

Handles user interactions like buying/selling and updates the view accordingly.

Event Flow Summary:

User Action (Buy/Sell) -> Controller updates Model -> Model triggers Event -> UIView updates UI
