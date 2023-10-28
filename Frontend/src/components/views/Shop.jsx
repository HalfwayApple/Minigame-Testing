import React, { useContext } from 'react';
import { GameContext } from '../../contexts/GameContext';
import { useNavigate } from "react-router-dom";
import './Shop.css';


const Shop = () => {
    const { currentGameState, currentItems, getInventory, getEquipmentForSale, returnToTown, } = useContext(GameContext);

    const displayInventory = () => {
        getInventory();
    }
    getEquipmentForSale();

    const navigate = useNavigate();
    const handleClick = () => { navigate('/PlayGame'); returnToTown(); currentGameState.location.name = "Town"; }

    return (
    <div className="game-container">
        {currentGameState ? (
            <div className="shop-container">

            <div>
                <h1 className="game-area-text">You are currently in: {currentGameState.location.name}</h1>
                <h2>Shop Inventory</h2>
            {/* <h2>for sell: {currentGameState.location.equipmentForSale[1]}</h2 */}
            <ul id="text-display" className="action-flow">
            {currentItems.map(listItem => {
                    return <li key={currentItems.indexOf(listItem)}>{listItem.name} + {listItem.attackPower}{listItem.armorValue} 
                    <button 
                    onClick={() => {
                        currentItems.indexOf(listItem);
                    }}>Buy</button></li>
                })
            }
            </ul>

            <h2>Your Inventory</h2>
            <ul id="text-display" className="action-flow">
                {currentGameState.hero.equipmentInBag.map((item, index) => (
                    <li key={index}>
                        {item.name} + {item.attackPower}{item.armorValue}
                        <button onClick={() => {/* sell */}}>Sell</button>
                    </li>
                ))}
            </ul>

            </div>
            <button className="button" onClick={() => handleClick()}>Leave</button>
        </div>
            
        ) : (
            <p>Error Loading game</p>
        )}
      
    </div>
  )
}

export default Shop;
