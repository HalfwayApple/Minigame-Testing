import React, { useContext } from 'react';
import { GameContext } from '../../contexts/GameContext';
import './Shop.css';

const Shop = ({ exitShop }) => {
    const { currentGameState } = useContext(GameContext);

    return (
        <div className="shop-container">
            <h2>Shop Inventory</h2>
            <ul>
                {currentGameState.shop.EquipmentForSale.map((item, index) => (
                    <li key={index}>
                        {item.name} - Price: {item.price}
                        <button onClick={() => {/* buy */}}>Buy</button>
                    </li>
                ))}
            </ul>

            <h2>Your Inventory</h2>
            <ul>
                {currentGameState.hero.equipmentInBag.map((item, index) => (
                    <li key={index}>
                        {item.name}
                        <button onClick={() => {/* sell */}}>Sell</button>
                    </li>
                ))}
            </ul>

            <button onClick={exitShop}>Exit Shop</button>  // Using the prop to exit the shop
        </div>
    );
}

export default Shop;
