import React, { useEffect, useState } from 'react'
import axios from 'axios'

export interface IRiderPage {
    driverId: number
}

// does not work
function RiderPage(props: IRiderPage) {

    const [user, setUser] = useState();
    const [loading, setLoading] = useState(true);



    useEffect(() => {
        console.log("???");
        axios.get("https://localhost:5001/get-user/" + props.driverId).then((res: any) => {
            console.log("USER LOADED");
            console.log(res);
            setUser(res.data);

            setLoading(false);
        })    

    }, [])

    if (loading) {
        return (
            
            <div>
                LOADING
            </div>
        )
    }
    return (
        <>
            User loaded... {props.driverId}
        </>
    )

}

export default RiderPage;