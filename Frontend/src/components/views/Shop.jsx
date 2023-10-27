import React, { useContext } from 'react';
import { GameContext } from '../../contexts/GameContext';
import './Shop.css';


const Shop = ({ exitShop }) => {
    const { currentGameState } = useContext(GameContext);

    return (
        <div className="shop-container">
            <h2>Shop Inventory</h2>
            <ul>
            {currentGameState.map(listItem => {
                    return <li key={currentGameState.indexOf(listItem)}>{listItem.name} + {listItem.attackPower}{listItem.armorValue} 
                    <button 
                    onClick={() => {
                        currentGameState.indexOf(listItem);
                    }}>Equip</button></li>
                })
            }
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

            <button onClick={exitShop}>Exit Shop</button>
        </div>
    );
}

export default Shop;
