-- Insert Dummy Data for Menus
INSERT INTO Menus (MenuName, Description, DisplayOrder, IsActive) VALUES
('Starters', 'Begin your meal with our delicious starters.', 1, 1),
('Main Course', 'Hearty main dishes to satisfy your hunger.', 2, 1),
('Desserts', 'Sweet treats to finish your dining experience.', 3, 1),
('Beverages', 'Refreshing drinks and curated wines.', 4, 1);

-- Insert Dummy Data for RestaurantTables (20 records)
INSERT INTO RestaurantTables (TableNumber, SeatingCapacity, Location, PhotoPath, IsActive) VALUES
('T01', 2, 'Window', '/assets/tables/t1.jpg', 1),
('T02', 2, 'Window', '/assets/tables/t2.jpg', 1),
('T03', 4, 'Center', '/assets/tables/t3.jpg', 1),
('T04', 4, 'Center', '/assets/tables/t4.jpg', 1),
('T05', 4, 'Balcony', '/assets/tables/t5.jpg', 1),
('T06', 6, 'Balcony', '/assets/tables/t6.jpg', 1),
('T07', 6, 'Corner', '/assets/tables/t7.jpg', 1),
('T08', 8, 'Corner', '/assets/tables/t8.jpg', 1),
('T09', 2, 'Bar', '/assets/tables/t9.jpg', 1),
('T10', 2, 'Bar', '/assets/tables/t10.jpg', 1),
('T11', 4, 'Patio', '/assets/tables/t11.jpg', 1),
('T12', 4, 'Patio', '/assets/tables/t12.jpg', 1),
('T13', 2, 'Terrace', '/assets/tables/t13.jpg', 1),
('T14', 2, 'Terrace', '/assets/tables/t14.jpg', 1),
('T15', 6, 'Patio', '/assets/tables/t15.jpg', 1),
('T16', 6, 'Center', '/assets/tables/t16.jpg', 1),
('T17', 8, 'Private Room 1', '/assets/tables/t17.jpg', 1),
('T18', 10, 'Private Room 2', '/assets/tables/t18.jpg', 1),
('T19', 4, 'Window', '/assets/tables/t19.jpg', 1),
('T20', 4, 'Corner', '/assets/tables/t20.jpg', 1);

-- Insert Dummy Data for Items (20 records)
INSERT INTO Items (MenuId, ItemName, Description, Price, Currency, Ingredients, Origin, IsAvailable) VALUES
(1, 'Bruschetta', 'Fresh tomato, basil, and garlic on toasted bread.', 8.50, '$', 'Tomato, Basil, Garlic, Olive Oil, Bread', 'Italy', 1),
(1, 'Calamari', 'Grilled calamari rings with lemon and parsley.', 12.00, '$', 'Calamari, Lemon, Parsley, Olive Oil', 'Mediterranean', 1),
(1, 'Garlic Bread', 'Oven baked bread infused with garlic and butter.', 5.50, '$', 'Bread, Garlic, Butter, Parsley', 'Italy', 1),
(1, 'Stuffed Mushrooms', 'Mushrooms stuffed with cheese and herbs.', 9.00, '$', 'Mushrooms, Cream Cheese, Herbs, Breadcrumbs', 'France', 1),
(1, 'Caesar Salad', 'Crisp romaine lettuce with Caesar dressing and croutons.', 10.00, '$', 'Romaine, Parmesan, Croutons, Caesar Dressing', 'Mexico', 1),
(2, 'Grilled Salmon', 'Perfectly grilled salmon fillet with asparagus.', 24.00, '$', 'Salmon, Asparagus, Lemon, Olive Oil', 'Atlantic', 1),
(2, 'Ribeye Steak', 'Premium beef steak cooked to perfection.', 35.00, '$', 'Beef Ribeye, Rosemary, Butter, Garlic', 'USA', 1),
(2, 'Chicken Alfredo', 'Rich creamy pasta with grilled chicken slices.', 18.50, '$', 'Pasta, Chicken, Cream, Parmesan, Garlic', 'Italy', 1),
(2, 'Vegetarian Moussaka', 'Layered eggplant and potato with bechamel sauce.', 16.00, '$', 'Eggplant, Potato, Tomato, Bechamel', 'Greece', 1),
(2, 'Lamb Chops', 'Tender lamb chops with a red wine reduction.', 29.00, '$', 'Lamb, Red Wine, Thyme, Garlic', 'New Zealand', 1),
(2, 'Seafood Paella', 'Traditional Spanish rice dish with mixed seafood.', 26.00, '$', 'Rice, Saffron, Shrimp, Mussels, Lemon', 'Spain', 1),
(2, 'Beef Burger', 'Juicy beef patty with cheese, lettuce, and tomato.', 15.00, '$', 'Beef, Buns, Cheddar, Lettuce, Tomato', 'USA', 1),
(3, 'Tiramisu', 'Classic Italian coffee-flavored dessert.', 9.50, '$', 'Mascarpone, Coffee, Cocoa, Savoiardi', 'Italy', 1),
(3, 'Cheesecake', 'New York style cheesecake with strawberry topping.', 8.00, '$', 'Cream Cheese, Graham Cracker, Strawberries', 'USA', 1),
(3, 'Chocolate Lava Cake', 'Warm chocolate cake with a gooey center.', 10.50, '$', 'Chocolate, Butter, Flour, Eggs', 'France', 1),
(3, 'Panna Cotta', 'Creamy Italian vanilla dessert with berry coulis.', 8.50, '$', 'Cream, Sugar, Gelatin, Berries', 'Italy', 1),
(4, 'Espresso', 'Strong, rich Italian coffee.', 3.50, '$', 'Coffee Beans, Water', 'Italy', 1),
(4, 'Cappuccino', 'Espresso with steamed milk foam.', 4.50, '$', 'Coffee, Milk', 'Italy', 1),
(4, 'Fresh Orange Juice', 'Freshly squeezed oranges.', 5.00, '$', 'Oranges', 'Local', 1),
(4, 'Red Wine Glass', 'House red wine, full-bodied.', 8.00, '$', 'Grapes', 'France', 1);

-- Insert Dummy Data for ItemPhotos
INSERT INTO ItemPhotos (ItemId, PhotoPath, IsPrimary) VALUES
(1, 'https://images.unsplash.com/photo-1572695157366-5e585ab2b69f?auto=format&fit=crop&w=800&q=80', 1), 
(2, 'https://images.unsplash.com/photo-1604908177651-4191fe42f36d?auto=format&fit=crop&w=800&q=80', 1),
(3, 'https://images.unsplash.com/photo-1573140247632-f8fd74997d5c?auto=format&fit=crop&w=800&q=80', 1), 
(4, 'https://images.unsplash.com/photo-1518133683791-0b9de5a055f0?auto=format&fit=crop&w=800&q=80', 1),
(5, 'https://images.unsplash.com/photo-1550304943-4f24f54ddde9?auto=format&fit=crop&w=800&q=80', 1), 
(6, 'https://images.unsplash.com/photo-1467003909585-2f8a72700288?auto=format&fit=crop&w=800&q=80', 1),
(7, 'https://images.unsplash.com/photo-1600891964092-4316c288032e?auto=format&fit=crop&w=800&q=80', 1), 
(8, 'https://images.unsplash.com/photo-1645112411341-6c4fd023714a?auto=format&fit=crop&w=800&q=80', 1),
(9, 'https://images.unsplash.com/photo-1633337474564-9d90ec7d03a9?auto=format&fit=crop&w=800&q=80', 1), 
(10, 'https://images.unsplash.com/photo-1603048297172-c92544798d5e?auto=format&fit=crop&w=800&q=80', 1),
(11, 'https://images.unsplash.com/photo-1534080564583-6be75777b70a?auto=format&fit=crop&w=800&q=80', 1), 
(12, 'https://images.unsplash.com/photo-1568901346375-23c9450c58cd?auto=format&fit=crop&w=800&q=80', 1),
(13, 'https://images.unsplash.com/photo-1571115177098-24edf1c210ed?auto=format&fit=crop&w=800&q=80', 1), 
(14, 'https://images.unsplash.com/photo-1524351199678-941a58a3df50?auto=format&fit=crop&w=800&q=80', 1),
(15, 'https://images.unsplash.com/photo-1624353365286-3f8d62daad51?auto=format&fit=crop&w=800&q=80', 1), 
(16, 'https://images.unsplash.com/photo-1488477181946-6428a0291777?auto=format&fit=crop&w=800&q=80', 1),
(17, 'https://images.unsplash.com/photo-1510591509098-f4fdc6d0fd24?auto=format&fit=crop&w=800&q=80', 1), 
(18, 'https://images.unsplash.com/photo-1534045558197-28d54203e061?auto=format&fit=crop&w=800&q=80', 1),
(19, 'https://images.unsplash.com/photo-1613478223719-2ab802602423?auto=format&fit=crop&w=800&q=80', 1), 
(20, 'https://images.unsplash.com/photo-1506377247377-2a5b3b417ebb?auto=format&fit=crop&w=800&q=80', 1);

-- Insert Dummy Data for Reservations
INSERT INTO Reservations (TableId, GuestName, GuestEmail, GuestPhone, ReservationDate, EndTime, NumberOfGuests, Status, Notes) VALUES
(1, 'Alice Smith', 'alice@test.com', '555-0101', '2026-05-20 18:30:00', '2026-05-20 20:30:00', 2, 'Confirmed', 'Window seat please'),
(3, 'Bob Johnson', 'bob@test.com', '555-0102', '2026-05-21 19:00:00', '2026-05-21 21:00:00', 4, 'Pending', 'Birthday celebration'),
(6, 'Charlie Davis', 'charlie@test.com', '555-0103', '2026-05-22 20:00:00', '2026-05-22 22:30:00', 6, 'Confirmed', NULL);

-- Insert Dummy Data for Feedbacks
INSERT INTO Feedbacks (ReservationId, GuestName, Comment, VisitRating, IsApproved) VALUES
(1, 'Alice Smith', 'Amazing experience, the salmon was perfect!', 5, 1),
(NULL, 'Anonymous Diner', 'Service was slightly slow but food was good.', 4, 1),
(3, 'Charlie Davis', 'Great for groups, we loved the private feel.', 5, 1);