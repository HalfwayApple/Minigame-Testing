import React, { useContext } from 'react';
import { GameContext } from '../../contexts/GameContext';
import { useNavigate } from "react-router-dom";
import './Shop.css';


const Shop = () => {
    const { currentGameState, currentItems, getInventory, enterStore } = useContext(GameContext);

    const displayInventory = () => {
        getInventory();
    }

    const navigate = useNavigate();
    const handleClick = () => { navigate('/PlayGame'); currentGameState.location.name = "Town"; }

    return (
        <div className="shop-container">
            <h2>Shop Inventory</h2>
            <ul>
            {currentItems.map(listItem => {
                    return <li key={currentItems.indexOf(listItem)}>{listItem.name} + {listItem.attackPower}{listItem.armorValue} 
                    <button 
                    onClick={() => {
                        currentItems.indexOf(listItem);
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

            <div>
                <h1 className="game-area-text">You are currently in: {currentGameState.location.name}</h1>
                <ul id="text-display" className="action-flow">
                    { 
                        currentItems.map(listItem => {
                            return <li key={currentItems.indexOf(listItem)}>{listItem.name} + {listItem.attackPower}{listItem.armorValue} 
                            <button 
                            onClick={() => {
                                currentItems.indexOf(listItem);
                            }}>Equip</button></li>
                        })
                    }
                    
                </ul>
                <ul id="text-display" className="action-flow">
                    { 
                        currentItems.map(listItem => {
                            return <li key={currentItems.indexOf(listItem)}>{listItem.name}
                            <button 
                            onClick={() => {
                                currentItems.indexOf(listItem);
                            }}>Equip</button></li>
                        })
                    }
                    
                </ul>
                <div className="button-container">
                    
                    {currentGameState.location.name !== "Battle" &&
                        <button className="game-button" onClick={() => {
                            displayInventory();
                        }}>Check inventory</button>
                    }

                    {currentGameState.location.name !== "Battle" &&
                        <button className="game-button" onClick={() =>{
                            // enterStore();
                            // handleClick();
                        }}>Enter Store</button>
                    }
                    
                </div>
            </div>
            <button className="button" onClick={() => handleClick()}>Leave</button>
        </div>

        
    );
}

export default Shop;
