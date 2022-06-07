import React, { useEffect, useState } from 'react'
import { IPage } from '../../App';
import NoUserErrorPage from '../shared/NoUserError/NoUserErrorPage';

function RatingsPage(props: IPage) {

    const [error, setError] = useState<boolean>(false);

    useEffect(() => {
        if (!props.user) {
            setError(true);
        }
    }, []);

    if (error) return <NoUserErrorPage />

    return (
        <>
            <div>
                Ratings page
            </div>
        </>
    )

}

export default RatingsPage;