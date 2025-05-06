#INVENTORY SYSTEM

Design Pattern Used:

MVC: Player and Shop are structured with separate Models (data), Controllers (logic), and Views (UI).

Observer Pattern: EventService is used to update UI views reactively based on model changes.

Player System:

PlayerModel holds coins, items, and carry weight.

PlayerController handles buying/selling logic, checks limits, updates the model, and fires events.

Shop System:

ShopModel holds shop inventory.

ShopController manages item transfers to/from the player and inventory updates.

UI System (UIView):

Subscribes to events via EventService.

Updates coins, carry weight, inventory slots, and popup messages.

Dynamically populates UI elements using data from the player/shop models.

Event Flow:

User clicks a button (Buy/Sell) → Controller updates Model → Model triggers event → UIView reacts and updates UI.
