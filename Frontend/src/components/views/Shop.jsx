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
    <div className="game-container">
        {currentGameState ? (
            <div className="shop-container">
            
            <h2>Shop Inventory</h2>
            {/* <h2>for sell: {currentGameState.location.equipmentForSale[1]}</h2 */}
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
                            }}>Sell</button></li>
                        })
                    }
                    
                </ul>
                <ul id="text-display" className="action-flow">
                    { 
                        currentItems.map(listItem => {
                            return <li key={currentItems.indexOf(listItem)}>{listItem.name} + {listItem.attackPower}{listItem.armorValue} 
                            <button 
                            onClick={() => {
                                currentItems.indexOf(listItem);
                            }}>Buy</button></li>
                        })
                    }
                    
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
