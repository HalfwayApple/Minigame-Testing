import React, {useContext} from 'react'
import './WelcomePage.css';
import { GameContext } from '../../contexts/GameContext';

const WelcomePage = () => {
    const {handleNavigateSite} = useContext(GameContext);

    const handleClick = () => {
        setTimeout(() => {
            handleNavigateSite('/PlayGame');
        }, 500);
    }

    return (
        <div className="welcome-container">
            <button className="button" id='startgame-button' onClick={() => handleClick()}>Start Game</button>
        </div>
    )
}

export default WelcomePage
